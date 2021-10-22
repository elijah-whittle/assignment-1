using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform frontFeet;
    public Transform backFeet;
    public bool grounded = false;
    public bool grounded2 = false;
    float dif = 1;

    public int enemySpeed = 2;
    public int sightRange = 3;
    public int dir = -1;
    public int health = 10;
    public LayerMask playerLayer;
    Rigidbody2D rb;
    Animator anim;
    enum State
    {
        Idle,
        Walk,
        Follow,
        Die
    }

    State currentState;
    SpriteRenderer rend;
    //----slow cooldown----//
    public float max_cd = 3;
    public float curr_cd = 3;

    void Start()
    {
        //Vector2 currPos = transform.position;
        //float moveRight = transform.position.x;
        //float moveLeft = -transform.position.x;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        //StartCoroutine(NewState());
    }
    /*
    IEnumerator NewState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, sightRange, playerLayer);
            //currentState = (State)Random.Range(0, 2);
            if (players.Length > 0)
            {
                if (players[0].transform.position.x > transform.position.x)
                {
                    dir = 1;
                }
                else
                {
                    dir = -1;
                }
                currentState = State.Follow;
            }
            else
            {
                currentState = (State)Random.Range(0, 2);
                dir *= -1;
            }
            transform.localScale = new Vector2(dir, 1);
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Walk:
                rb.velocity = new Vector2(enemySpeed * dir, rb.velocity.y);
                break;
            case State.Follow:
                rb.velocity = new Vector2(3 * enemySpeed * dir, rb.velocity.y);
                break;
            default:
                break;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ice"))
        {
            health -= 1;
            enemySpeed = 0;
        }
        if (other.gameObject.CompareTag("Spell") || other.gameObject.CompareTag("attack_spell"))
        {
            health -= 5;
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            health -= 2;
            Destroy(other.gameObject);
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
            if (dir == 1)
            {
                dir = -1;
            }
            else
            {           
                dir = 1;
            }
            rb.velocity = new Vector2(Mathf.Abs(dif) * enemySpeed * dir, rb.velocity.y);
        }

        if (!grounded)
        {
            dif = rb.position.x - frontFeet.position.x;
        }
        else if (!grounded2)
        {
            dif = rb.position.x - backFeet.position.x;
        }

        //float dif = rb.position.x - frontFeet.position.x;

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
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (enemySpeed == 0)
        {
            curr_cd -= Time.deltaTime;
            if (curr_cd <= 0)
            {
                enemySpeed = 2;
                //ifCD = false;
                curr_cd = max_cd;
            }
        }
        //gameObject.position.x += enemySpeed;
    }
}
