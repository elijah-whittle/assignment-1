using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch_bullet : MonoBehaviour
{
    public int dur = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, dur);
    }
}
