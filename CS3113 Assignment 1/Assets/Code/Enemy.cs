using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int enemySpeed = 10;
    public int dir = 1;
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        //Vector2 currPos = transform.position;
        //float moveRight = transform.position.x;
        //float moveLeft = -transform.position.x;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spell"))
        {
            health -= 1;
            enemySpeed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        //gameObject.position.x += enemySpeed;
    }
}
