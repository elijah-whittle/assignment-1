using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    private GameObject lightPos;

    private Vector2 lightLocation;

    public float speed; 

    void start(){
        lightPos = GameObject.Find("firePos");
    }

    void update(){
        lightLocation = new Vector2(lightPos.transform.position.x, lightPos.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, lightLocation, speed *Time.deltaTime);
    }

}
