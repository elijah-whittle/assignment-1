using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch : MonoBehaviour
{
    private GameObject location;
    private Vector2 locationPos; 

    public float speed = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        location = GameObject.Find("Torch");
    }

    // Update is called once per frame
    void Update()
    {
        locationPos = new Vector2(location.transform.position.x, location.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, locationPos, speed * Time.deltaTime); 
    }
}

