using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{
    public int health = 100; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(health <= 0){
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Fire"){
            Destroy(other.gameObject);
            health -= 20;
            
        }
    }
        
}
