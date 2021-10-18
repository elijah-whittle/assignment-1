using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch_lifebarcontroler : MonoBehaviour
{
    public GameObject Player;
    public GameObject witch;
    public GameObject lifebarBG;
    public GameObject lifebar;
    public GameObject icon;
    private float y_distance;
    // Start is called before the first frame update
    void Start()
    {
        lifebarBG.SetActive(false);
        lifebar.SetActive(false);
        icon.SetActive(false);
        y_distance = Mathf.Abs(Player.GetComponent<Transform>().position.y - witch.GetComponent<Transform>().position.y);
    }

    // Update is called once per frame
    void Update()
    {
        y_distance = Mathf.Abs(Player.GetComponent<Transform>().position.y - witch.GetComponent<Transform>().position.y);
        bool ifexist = GameObject.Find("witch");
        if (y_distance<=7f){
            lifebarBG.SetActive(true);
            lifebar.SetActive(true);
            icon.SetActive(true);
        }
    }
}
