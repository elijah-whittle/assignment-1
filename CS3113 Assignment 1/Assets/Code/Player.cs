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
    /*-------------------------------*/
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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

        if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);

        // Earth
        Collider2D earthTouch = Physics2D.OverlapCircle(feet.position, .3f, earthLayer);
        if (earthTouch)
        {
            GameObject earthSpot = earthTouch.gameObject;
            float vert = Input.GetAxis("Vertical") * Time.deltaTime;
            earthTouch.gameObject.transform.position = new Vector2(earthTouch.gameObject.transform.position.x,
                                                                   earthTouch.gameObject.transform.position.y
                                                                   + vert / 2);
            earthTouch.gameObject.transform.localScale = new Vector3(earthTouch.gameObject.transform.localScale.x,
                                                                     earthTouch.gameObject.transform.localScale.y
                                                                     + vert,
                                                                     earthTouch.gameObject.transform.localScale.z);
        }
        // TODO convert to a function
        Collider2D sideEarthTouch = Physics2D.OverlapCircle(front.position, .41f, earthLayer);
        if (sideEarthTouch)
        {
            GameObject earthSpot = sideEarthTouch.gameObject;
            float vert = Input.GetAxis("Vertical") * Time.deltaTime;
            sideEarthTouch.gameObject.transform.position = new Vector2(sideEarthTouch.gameObject.transform.position.x,
                                                                   sideEarthTouch.gameObject.transform.position.y
                                                                   + vert / 2);
            sideEarthTouch.gameObject.transform.localScale = new Vector3(sideEarthTouch.gameObject.transform.localScale.x,
                                                                     sideEarthTouch.gameObject.transform.localScale.y
                                                                     + vert,
                                                                     sideEarthTouch.gameObject.transform.localScale.z);
        }


        if ((grounded || earthTouch) && Input.GetButtonDown("Jump"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }

    }
}
