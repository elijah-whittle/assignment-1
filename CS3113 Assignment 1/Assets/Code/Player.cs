//using System.Threading.Tasks.Dataflow;
//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int speed = 4;
    public int jumpForce = 500;

    public LayerMask groundLayer;
    public Transform feet;
    public bool grounded = false;

    float xSpeed;
    Rigidbody2D _rigidbody;

    bool[] spells = { false, false, false, false };

    /********** Paper Scraps ************/
    public string[] levels = { "wind", "water", "earth", "fire" };
    public bool paperCollected = false;
    public int currentLevel = 0;
    /***********************************/


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
        SpriteRenderer _sprite = earthChunk.GetComponent<SpriteRenderer>();
        earthChunk.transform.position = new Vector2(earthChunk.transform.position.x,
                                                    earthChunk.transform.position.y + vert / 2);
        _sprite.size = new Vector2(_sprite.size.x, _sprite.size.y + vert);

    }
    /*-------------------------------*/

    /*------------ FIRE -------------*/

    public Transform firePos;
    public GameObject fireBall;
    public bool left = false;

    public Transform lightPos;
    public GameObject lightBall;

    /*-------------------------------*/

    /*------------ WIND ------------*/
    public GameObject Wind_blade;
    public Transform wind_blade_pos;
    public GameObject Wind_shield;
    bool Wind_power = true;
    void closewindshield(){
        Wind_shield.GetComponent<Renderer>().enabled = false;
        Wind_shield.GetComponent<Collider2D>().enabled = false;
    }
    /*-------------------------------*/

    /*------------ WATER ------------*/
    public GameObject icePrefab;
    public GameObject healPrefab;
    public int castingForce = 800;

    /*-------------------------------*/


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //lightPos = GetComponent<Transform>();
        Wind_shield.GetComponent<Renderer>().enabled = false;
        Wind_shield.GetComponent<Collider2D>().enabled = false;
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
            _rigidbody.velocity = Vector2.zero;
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
        if (Input.GetMouseButtonDown(0)){
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

        /* Wind */
        if(Input.GetKeyDown (KeyCode.I)){
            if(Wind_power){
                GameObject newwind_blade = Instantiate(Wind_blade,wind_blade_pos.position,Quaternion.identity);
            }
        }
        if(Input.GetKeyDown (KeyCode.O)){
            if(Wind_power){
                Wind_shield.GetComponent<Renderer>().enabled = true;
                Wind_shield.GetComponent<Collider2D>().enabled = true;
                Invoke("closewindshield", 2);
            }
        }

        /* Water */
        //if (Input.GetButtonDown("Fire1"))
        if(Input.GetKeyDown (KeyCode.R))
        {
            GameObject newBlast = Instantiate(icePrefab, transform.position, Quaternion.identity);
            newBlast.GetComponent<Rigidbody2D>().AddForce(new Vector2(castingForce * transform.localScale.x, 0));
        }

        //if (Input.GetButtonDown("Fire3"))
        if(Input.GetKeyDown (KeyCode.E))
        {
            GameObject healing = Instantiate(healPrefab, feet.position, Quaternion.identity);

        }

        // jump
        if ((grounded || earthTouch) && Input.GetButtonDown("Jump"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    /*
     * Spell/Door
     */
    //private void OnTriggerEnter2D(Collider2D collision)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spell"))
        {
            print("you got a spell");
            paperCollected = true;
            spells[currentLevel] = true; // TODO after this week, change this line
            Destroy(collision.gameObject);
        }
        if (paperCollected)
        {
            if (collision.gameObject.CompareTag("Door"))
            {
                currentLevel = (currentLevel + 1) % 4;
                paperCollected = false;
                SceneManager.LoadScene(levels[currentLevel]);
            }
        }
        if(collision.tag == "wind_pill"){
            Wind_power = true;
        }

    }
}
