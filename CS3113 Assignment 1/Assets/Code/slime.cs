using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    // Start is called before the first frame update

    int health = 20;
    int speed = 3;
    int direction = -1;

    public LayerMask playerLayer;
    Rigidbody2D rb2d;
    SpriteRenderer spriteR;
    Animator slimeAni;
    enum State{
        Idle, 
        Walk, 
        Jump
    }
    State currentState;

    public bool grounded = false;
    public Transform feet;
    public LayerMask groundLayer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        slimeAni = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    IEnumerator NewState(){
        while(true){
            yield return new WaitForSeconds(2);
            currentState = (State)Random.Range(0,3);
            direction *= -1;
        }
    }

    private void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);

        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 3, playerLayer);
        if(players.Length > 0){
            
            if(players[0].transform.position.x > transform.position.x){
                spriteR.flipX = false;
                direction = 1;
            }
            else{
                spriteR.flipX = true;
                direction = -1;
            }
            currentState = State.Jump;
        }

        switch(currentState){
            case State.Walk:
                if(direction == 1){
                    spriteR.flipX = true;
                }
                else{
                    spriteR.flipX = false;
                }
                rb2d.velocity= new Vector2(speed * direction, rb2d.velocity.y);
                slimeAni.SetFloat("Speed", 1);
                slimeAni.SetFloat("Jump", 0);
                break;

            case State.Jump:

                if(grounded){
                    if(direction == 1){
                        spriteR.flipX = true;
                    }
                    else{
                        spriteR.flipX = false;
                    }
                    rb2d.velocity= new Vector2(speed * direction, speed);
                    slimeAni.SetFloat("Jump", 1);
                }
                break;



            default:
                slimeAni.SetFloat("Speed", 0);
                slimeAni.SetFloat("Jump", 0);
                break;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Fire"||other.gameObject.tag == "ice"|| other.gameObject.tag == "attack_spell"){
            if(health >0){
                health -= 5;
                Destroy(other.gameObject);
            }
            else{
                Destroy(gameObject);
            }
        }
    }

}
