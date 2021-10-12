using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{

    Transform pos;
    public float speed;

    void Start(){
        pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lightPos;
    }
    void update(){
        transform.position = Vector2.MoveTowards(transform.position, pos.position, speed * Time.deltaTime);
    }


}
