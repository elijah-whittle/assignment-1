using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro_witch : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            for(int i = 0; i < PublicVars.spells.Length; ++i){
                PublicVars.spells[i] = false;
            }
        }
        Destroy(gameObject);
    }
}
