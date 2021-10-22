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

    public float max_cd_heal = 5;
    public float curr_cd_heal = 5;
    bool ifCD_heal = false;
    /*-------------------------------*/
    //public AudioManager audio_man;
    public AudioSource jump;
    public AudioSource water;
    public AudioSource fire;
    public AudioSource wind;
    public AudioSource walk;

    // Start is called before the first frame update
    void Start()
    {
        //PublicVars.currentLevel = SceneManager.GetActiveScene().buildIndex;
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
    private float Timebetween = 0.5f;
    float _Timer;
    void mg_deduct(){
        mp -=10;
    }
    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        anim.SetFloat("Speed", xSpeed);
        
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hp = 100;
        mp = 100;
        alive = true;
    }

    bool Cooldown(float time)
    {
        bool onCD = true;
        float curr_cdtime = time;
        while (onCD) {
            curr_cdtime -= Time.deltaTime;
            if (curr_cdtime <= 0)
            {
                onCD = false;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PublicVars.paused) { return; }

        //if your hp hits
        if (hp <= 0)
        {
            alive = false;

            //anim.SetBool("isAlive", alive);

            //Cooldown(3);
            ResetScene();
            /*
            if (SceneManager.GetActiveScene() == 
            SceneManager.LoadScene("water");*/
        }
        anim.SetBool("isAlive", alive);

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
            //anim.SetFloat("Speed", Mathf.Abs(xSpeed));
        }
        else
        {
            transform.localScale *= new Vector2(1, 1);
        }
        anim.SetFloat("Speed", Mathf.Abs(xSpeed));

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
        _Timer+=Time.deltaTime;
        if (_Timer>Timebetween){
                    if(mp<100){
                        mp+=1;
                    _Timer = 0;
                    }
        }
        Collider2D earthTouch = Physics2D.OverlapCircle(feet.position, .5f, earthLayer);
        //spell
        if (mp >= 10)
        {
            /* Fire */
            if (magic == Magic.Fire && PublicVars.spells[fireLoc])
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject fire = Instantiate(fireBall);
                    fire.GetComponent<fire>().shoot(left);
                    fire.transform.position = firePos.transform.position;
                    mg_deduct();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    GameObject torch = Instantiate(lightBall);
                    torch.transform.position = lightPos.transform.position;
                    Destroy(torch, 10.0f);
                    mg_deduct();
                }
            }

            /* Earth */

            if (magic == Magic.Earth && PublicVars.spells[earthLoc])
            {
                float vert = Input.GetAxis("Vertical") * Time.deltaTime;
                if (vert != 0f && earthTouch)
                {
                    earthTouch.gameObject.GetComponent<earth>().stretch(vert);
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
                        mg_deduct();
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
                if (ifCD_heal == false)
                {
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
                        mp -= 15;
                        ifCD_heal = true;
                        curr_cd_heal = max_cd;
                    }
                }
                else
                {
                    curr_cd_heal -= Time.deltaTime;
                    if (curr_cd_heal <= 0)
                    {
                        ifCD_heal = false;
                    }
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
                        mg_deduct();
                    }
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    if (Wind_power)
                    {
                        Wind_shield.GetComponent<Renderer>().enabled = true;
                        Wind_shield.GetComponent<Collider2D>().enabled = true;
                        Invoke("closewindshield", 2);
                        mg_deduct();
                    }
                }
            }

            // jump
            anim.SetBool("IsJump", grounded || earthTouch);
            if ((grounded || earthTouch) && Input.GetButtonDown("Jump"))
            {
                jump.Play();
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(new Vector2(0, jumpForce));
                //anim.SetBool("IsJump", true);
            }
        }
        
    }

    /*
     * Spell/Door
     */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Wind_shield.GetComponent<Collider2D>().enabled == false){
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
        }
        if (collision.gameObject.CompareTag("Spell"))
        {
            SpellItem spell = collision.gameObject.GetComponent<SpellItem>();
            paperCollected = true;
            spell.AddSpell();
            itemPanels[(int)spell.spell].GetComponent<ItemDisplay>().SetActive();
            Destroy(collision.gameObject);
        }
        if (paperCollected)
        {
            if (collision.gameObject.CompareTag("Door"))
            {
                paperCollected = false;
                SceneManager.LoadScene(collision.gameObject.GetComponent<Door>().nextLevel);
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
