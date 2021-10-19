using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candlecontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject yellowcandle;
    public GameObject bluecandle;
    public float Fullhealth = 50;
    private float health;
    void Start()
    {
        health = gameObject.GetComponent<witch>().health;
        yellowcandle.SetActive(true);
        bluecandle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   health = gameObject.GetComponent<witch>().health;
        if (health<Fullhealth/2){
            yellowcandle.SetActive(false);
            bluecandle.SetActive(true);
        }
    }
}
