using System;
using System.Runtime.Versioning;
//using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{

    public int damage = 20;
    public float speed = 20f;
    public Rigidbody2D rigid2D;
    public Vector2 vel;
    public SpriteRenderer spriteR;

    public bool leftCheck; 


    // Start is called before the first frame update
    public void shoot(bool left){
        if(left){
            spriteR.flipX = true;
            leftCheck = left;
            rigid2D.velocity = new Vector2(-speed, 0);
        }
        else{
            spriteR.flipX = false;
            leftCheck = left;
            rigid2D.velocity = new Vector2(speed, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rigid2D.velocity.y < vel.y){
            rigid2D.velocity = vel;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground"){
            if(leftCheck == true){
                rigid2D.velocity = new Vector2(-vel.x, -vel.y);
            }
            else{
                rigid2D.velocity = new Vector2(vel.x, -vel.y);
            }
        }
        Destroy(gameObject, 2.0f);
    }
    
}
