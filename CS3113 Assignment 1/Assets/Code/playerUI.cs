using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{
    public Image health_img;
    public Image mana_img;
	// Use this for initialization
    private float full_health;
    private float full_mana;
	void Start () {
        health_img = health_img.GetComponent<Image>();
        mana_img = mana_img.GetComponent<Image>();

        full_health = gameObject.GetComponent<Player>().hp;
        full_mana = gameObject.GetComponent<Player>().mp;
	}
	
	// Update is called once per frame
	void Update () {
        float curr_health = gameObject.GetComponent<Player>().hp;
        float curr_mana = gameObject.GetComponent<Player>().mp;
        health_img.fillAmount = curr_health / full_health;
        mana_img.fillAmount = curr_mana / full_mana;
    }
}
