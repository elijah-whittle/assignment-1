using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWhiteSlime : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform frontFeet;
    public Transform backFeet;
    public bool grounded = false;
    public bool grounded2 = false;
    float dif = 1;

    public int enemySpeed = 7;
    public int dir = -1;
    public LayerMask playerLayer;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer rend;
    //----slow cooldown----//
    public float max_cd = 3;
    public float curr_cd = 3;
    //---------------------//
    public float max_edge_timer = 0.05f;
    public float curr_time = 0.05f;
    bool ifturn = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ice"))
        {
            enemySpeed = 0;
        }
        if (other.gameObject.CompareTag("wall"))
        {
            if (dir == 1)
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }
            rb.velocity = new Vector2(4 * enemySpeed * dir, rb.velocity.y);
        }

    }


    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(frontFeet.position, .3f, groundLayer);
        grounded2 = Physics2D.OverlapCircle(backFeet.position, .3f, groundLayer);

        if (!grounded || !grounded2)
        {
            if (ifturn)
            {
                if (dir == 1)
                {
                    dir = -1;
                }
                else
                {
                    dir = 1;
                }
                ifturn = false;
            }
        }

        curr_time -= Time.deltaTime;
        if (curr_time <= 0)
        {
            ifturn = true;
            curr_time = max_edge_timer;
        }
        if (dir == 1)
        {
            //transform.localScale *= new Vector2(-1, 1);
            rend.flipX = false;
        }
        else
        {
            //transform.localScale *= new Vector2(1, 1);
            rend.flipX = true;
        }
        rb.velocity = new Vector2(Mathf.Abs(dif) * enemySpeed * dir, rb.velocity.y);
        if (enemySpeed == 0)
        {
            curr_cd -= Time.deltaTime;
            if (curr_cd <= 0)
            {
                enemySpeed = 2;
                curr_cd = max_cd;
            }
        };
    }
}
