using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    int speed = 3;
    int direction = -1;

    public LayerMask playerLayer;
    Rigidbody2D rb2d; 
    public SpriteRenderer spriteR;

    Animator catAni;

    enum State{
        Idle, 
        Walk, 
        Die
    }

    State currentState; 

    private void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        catAni = GetComponent<Animator>();
        StartCoroutine(NewState());
    }

    IEnumerator NewState(){
        while(true){
            yield return new WaitForSeconds(1);
            currentState = (State)Random.Range(0,2);
            direction *= -1;
        }
    }

    private void FixedUpdate()
    {
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
            currentState = State.Walk;
        }

        //transform.localScale = new Vector2(direction, 1);

        switch (currentState){
            case State.Walk:
                if(direction == 1){
                    spriteR.flipX = true;
                }
                else{
                    spriteR.flipX = false;
                }
                rb2d.velocity= new Vector2(speed * direction, rb2d.velocity.y);
                catAni.SetFloat("Speed", 1);
                break;

            default:
                catAni.SetFloat("Speed", 0);
                break;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Fire"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}