using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch_lifebarcontroler : MonoBehaviour
{
    public GameObject Player;
    public GameObject witch;
    private float x_distance;
    private float y_distance;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        x_distance = Mathf.Abs(Player.GetComponent<Transform>().position.x - witch.GetComponent<Transform>().position.x);
        y_distance = Mathf.Abs(Player.GetComponent<Transform>().position.y - witch.GetComponent<Transform>().position.y);
    }

    // Update is called once per frame
    void FixUpdate()
    {
        x_distance = Mathf.Abs(Player.GetComponent<Transform>().position.x - witch.GetComponent<Transform>().position.x);
        y_distance = Mathf.Abs(Player.GetComponent<Transform>().position.y - witch.GetComponent<Transform>().position.y);
        print(x_distance);
        bool ifexist = GameObject.Find("witch");
        if (ifexist == false || x_distance>15 || y_distance>7){
            gameObject.SetActive(false);
        }
        else{
            gameObject.SetActive(true);
        }
    }
}
