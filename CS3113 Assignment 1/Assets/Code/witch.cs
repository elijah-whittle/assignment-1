using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch : MonoBehaviour
{
    public int dir = 1;
    public int health = 50;
    public GameObject Player;
    public Transform fire_pos;
    public GameObject Bullet1;
    public int speed = 5;
    // Start is called before the first frame update
    private bool isattack = false;
    void stay(){
        isattack = false;
        transform.LookAt(Player.transform); 
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);
    }
    void move(){
        isattack = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed,GetComponent<Rigidbody2D>().velocity.x);
    }
    void attack(){
        transform.LookAt(Player.transform);
        isattack = true;
    }
    void Start()
    {
        //Vector2 currPos = transform.position;
        //float moveRight = transform.position.x;
        //float moveLeft = -transform.position.x;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<Animator>().SetFloat("speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
        gameObject.GetComponent<Animator>().SetBool("is_attack1", isattack);
        gameObject.GetComponent<Animator>().SetFloat("health", health);
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
