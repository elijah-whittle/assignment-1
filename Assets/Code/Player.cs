//using System.Threading.Tasks.Dataflow;
//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 4;
    public int jumpForce = 500;

    public LayerMask groundLayer;
    public Transform feet;
    public bool grounded = false;

    float xSpeed;
    Rigidbody2D _rigidbody;


    /*------------ EARTH ------------*/
    public LayerMask earthLayer;
    public Transform front;
    public bool touchingEarth = false;

    /*
     *  stretches a chunk of earth in the y direction
     *  inputs: 
     *      earthChunk      the chunk to stretch
     *      vert            how much to stretch it
     */
    void stretch(GameObject earthChunk, float vert)
    {
        earthChunk.transform.position = new Vector2(earthChunk.transform.position.x,
                                                    earthChunk.transform.position.y + vert / 2);
        earthChunk.transform.localScale = new Vector3(earthChunk.transform.localScale.x,
                                                      earthChunk.transform.localScale.y + vert,
                                                      earthChunk.transform.localScale.z);
    }
    /*-------------------------------*/

    /*------------ FIRE -------------*/

    public Transform firePos;
    public GameObject fireBall;
    public bool left = false;

    public Transform lightPos;
    public GameObject lightBall;

    /*-------------------------------*/


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //lightPos = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        // if you fall off the map
        if (transform.position.y < -10)
        {
            transform.position = Vector2.zero;
        }

        if (xSpeed < 0)
        {   
            left = true;
        }
        if (xSpeed >0)
        {
            left = false;
        }

        if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);

        /* Earth */
        float vert = Input.GetAxis("Vertical") * Time.deltaTime;
        Collider2D earthTouch = Physics2D.OverlapCircle(feet.position, .3f, earthLayer);
        Collider2D sideEarthTouch = Physics2D.OverlapCircle(front.position, .41f, earthLayer);
        if (vert != 0f)
        {
            if (earthTouch) { stretch(earthTouch.gameObject, vert); }
            if (sideEarthTouch) { stretch(sideEarthTouch.gameObject, vert); }
        }

        /* Fire */
        if(Input.GetMouseButtonDown(0)){
            GameObject fire = Instantiate(fireBall);
            fire.GetComponent<fire>().shoot(left);
             fire.transform.position = firePos.transform.position; 
        }

        if(Input.GetMouseButtonDown(1)){
            GameObject light = Instantiate(lightBall);
            //light.GetComponent<light>().torch(left);
            light.transform.position = lightPos.transform.position;
            Destroy(light, 10.0f);
        }

        // jump
        if ((grounded || earthTouch) && Input.GetButtonDown("Jump"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }
}
