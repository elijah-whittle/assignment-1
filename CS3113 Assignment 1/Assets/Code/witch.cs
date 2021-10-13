using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch : MonoBehaviour
{
    public int dir = 1;
    public float health = 50f;
    public GameObject Player;
    public Transform fire_pos;
    public GameObject Bullet1;
    public Animator witchanimator;
    public float speed = 1f;
    // Start is called before the first frame update
    private bool isattack = false;
    private bool left = true;
    void lookplayer(){
        if (gameObject.transform.position.x < Player.transform.position.x){
            transform.eulerAngles = new Vector3(0,180,0);
            left = false;
        }
        else{
            transform.eulerAngles = new Vector3(0,0,0);
            left = true;
        }
    }
    void stay(){
        lookplayer();
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        isattack = false;
    }
    void move(){
        lookplayer();
        if (left == true){
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed*-1,0f);
        }
        else{gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0f);}
        isattack = false;
    }
    void attack(){
        lookplayer();
        isattack = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
    }
    void Start()
    {
        //Vector2 currPos = transform.position;
        //float moveRight = transform.position.x;
        //float moveLeft = -transform.position.x;
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spell"))
        {
            health -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        witchanimator.SetFloat("speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
        witchanimator.SetBool("is_attack1", isattack);
        witchanimator.SetFloat("health", health);
        if(health >= 25){
            int rand = Random.Range(0,5);
            if (rand==0){
                stay();
            }
            else if (rand==1){
                move();
            }
            else{
                attack();
            }
        }
        if(health <= 25 & health>0){
            int rand = Random.Range(0,5);
            if (rand==0){
                stay();
            }
            else if (rand==1){
                move();
            }
            else{
                attack();
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        //gameObject.position.x += enemySpeed;
    }
}
