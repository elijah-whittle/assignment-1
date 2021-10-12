using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind_Player : MonoBehaviour
{
    public int speed = 4;
    public int jumpForce = 500;

    public LayerMask groundLayer;
    public Transform feet;
    public bool grounded = false;
    public GameObject Wind_blade;
    public Transform wind_blade_pos;
    public GameObject Wind_shield;

    bool Wind_power = false;
    float xSpeed;
    Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        if (transform.position.y < -10)
        {
            transform.position = Vector2.zero;
        }

        if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);

        if(grounded && Input.GetButtonDown("Jump"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
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
    }

    void closewindshield(){
        Wind_shield.GetComponent<Renderer>().enabled = false;
                Wind_shield.GetComponent<Collider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "wind_pill"){
            Wind_power = true;
        }
    }
}

    //make a ground layer and feet for player
    //Buttonup getbutton buttondown
