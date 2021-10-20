//using System.Threading.Tasks.Dataflow;
//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public enum Magic
    {
        None,
        Wind,
        Fire,
        Earth,
        Water
    }

    public Magic magic;
    public int speed = 4;
    public int jumpForce = 500;

    public LayerMask groundLayer;
    public Transform feet;
    public bool grounded = false;

    float xSpeed;
    Rigidbody2D _rigidbody;

    /*---------------- UI ----------------*/
    public GameObject[] itemPanels;

    void Select(int i)
    {
        for (int j = 0; j < itemPanels.Length; ++j)
        {
            itemPanels[j].GetComponent<ItemDisplay>().Deselect();
        }
        itemPanels[i].GetComponent<ItemDisplay>().Select();
    }
    /*------------------------------------*/



    /********** Paper Scraps ************/
    bool paperCollected = false;
    /***********************************/


    /*------------ EARTH ------------*/
    public LayerMask earthLayer;
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
    public Transform castPos;

    /*-------------------------------*/

    /*---------- Animation ----------*/
    public Animator anim;
    public bool isGrounded;

    /*-----------Player Stats--------*/
    public int mp = 100;
    public int hp = 100;
    public bool alive = true;
    /*-------------------------------*/
    public float max_cd = 3;
    public float curr_cd = 3;
    bool ifCD = false;

    // Start is called before the first frame update
    void Start()
    {
        PublicVars.currentLevel = SceneManager.GetActiveScene().buildIndex;
        _rigidbody = GetComponent<Rigidbody2D>();
        //lightPos = GetComponent<Transform>();
        Wind_shield.GetComponent<Renderer>().enabled = false;
        Wind_shield.GetComponent<Collider2D>().enabled = false;
        anim = gameObject.GetComponent<Animator>();

        for (int i = 0; i < PublicVars.spells.Length; ++i)
        {
            if (PublicVars.spells[i])
            {
                itemPanels[i].GetComponent<ItemDisplay>().SetActive();
            }
        }
    }

    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        anim.SetFloat("Speed", xSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //if your hp hits
        if (hp <= 0)
        {
            alive = false;
            /*
            if (SceneManager.GetActiveScene() == 
            SceneManager.LoadScene("water");*/
        }
        anim.SetBool("isAlive", alive);

        if (PublicVars.paused) { return; }

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
            anim.SetFloat("Speed", Mathf.Abs(xSpeed));
        }

        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);

        int windLoc = 3, fireLoc = 0, earthLoc = 1, waterLoc = 2;

        if (Input.GetKeyDown("1")) // Fire
        {
            magic = Magic.Fire;
            Select(fireLoc);
        }
        else if (Input.GetKeyDown("2")) // Earth
        {
            magic = Magic.Earth;
            Select(earthLoc);
        }
        else if (Input.GetKeyDown("3")) // Water
        {
            magic = Magic.Water;
            Select(waterLoc);
        }
        else if (Input.GetKeyDown("4")) // Wind
        {
            magic = Magic.Wind;
            Select(windLoc);
        }

        /* Earth */
        Collider2D earthTouch = Physics2D.OverlapCircle(feet.position, .5f, earthLayer);
        if (magic == Magic.Earth && PublicVars.spells[earthLoc])
        {
            float vert = Input.GetAxis("Vertical") * Time.deltaTime;
            if (vert != 0f && earthTouch) { stretch(earthTouch.gameObject, vert); }
        }

        /* Fire */
        if (magic == Magic.Fire && PublicVars.spells[fireLoc])
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject fire = Instantiate(fireBall);
                fire.GetComponent<fire>().shoot(left);
                fire.transform.position = firePos.transform.position;
            }

            if (Input.GetMouseButtonDown(1))
            {
                GameObject torch = Instantiate(lightBall);
                torch.transform.position = lightPos.transform.position;
                Destroy(torch, 10.0f);
            }
        }

        /* Wind */
        if (magic == Magic.Wind && PublicVars.spells[windLoc])
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Wind_power)
                {
                    GameObject newwind_blade = Instantiate(Wind_blade, wind_blade_pos.position, Quaternion.identity);
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (Wind_power)
                {
                    Wind_shield.GetComponent<Renderer>().enabled = true;
                    Wind_shield.GetComponent<Collider2D>().enabled = true;
                    Invoke("closewindshield", 2);
                }
            }
        }

        /* Water */
        if (magic == Magic.Water && PublicVars.spells[waterLoc])
        {
            //if (Input.GetButtonDown("Fire1"))
            if (ifCD == false)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    GameObject newBlast = Instantiate(icePrefab, castPos.position, Quaternion.identity);
                    newBlast.GetComponent<Rigidbody2D>().AddForce(new Vector2(castingForce * transform.localScale.x, 0));
                    //StartCoroutine(Cooldown());
                    ifCD = true;
                    curr_cd = max_cd;
                }
            }
            else
            {
                curr_cd -= Time.deltaTime;
                if (curr_cd <= 0)
                {
                    ifCD = false;
                }
            }


            //if (Input.GetButtonDown("Fire3"))
            if (Input.GetButtonDown("Fire2"))        //heals the player 
            {
                GameObject healing = Instantiate(healPrefab, feet.position, Quaternion.identity);
                if (hp <= 85)
                {
                    hp += 15;
                }
                else if (hp > 85)
                {
                    hp = 100;

                }
            }
        }

        // jump
        anim.SetBool("IsJump", grounded);
        if ((grounded || earthTouch) && Input.GetButtonDown("Jump"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            //anim.SetBool("IsJump", true);
        }
    }

    /*
     * Spell/Door
     */
    //private void OnTriggerEnter2D(Collider2D collision)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hp > 0)
            {
                if (hp <= 20)
                {
                    hp = 0;
                }
                else if (hp > 20) {
                    hp -= 20;
                }
            }
        }
        if (collision.gameObject.CompareTag("Spell"))
        {
            print("you got a spell");
            paperCollected = true;
            PublicVars.spells[PublicVars.currentLevel] = true; // TODO after this week, change this line
            itemPanels[PublicVars.currentLevel].GetComponent<ItemDisplay>().SetActive();
            Destroy(collision.gameObject);
        }
        if (paperCollected)
        {
            if (collision.gameObject.CompareTag("Door"))
            {
                PublicVars.currentLevel = (PublicVars.currentLevel + 1) % 4;
                paperCollected = false;
                SceneManager.LoadScene(PublicVars.levels[PublicVars.currentLevel]);
            }
        }
        if(collision.tag == "wind_pill"){
            Wind_power = true;
        }
        if (collision.tag == "mana")
        {
            if (mp >= 70)
            {
                mp = 100;
            }
            else
            {
                mp += 30;
            }
            Destroy(collision.gameObject);
        }

    }

    public void StopJump()
    {
        anim.SetBool("IsJump", false);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5);
    }
}
