using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drug_distruction : MonoBehaviour
{
    bool status = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            status = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(status){
            Destroy(gameObject);
        }
    }
}
