using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class witch_lifebar : MonoBehaviour {
    public Image img;
    public GameObject witch;
	// Use this for initialization
    private float full_health;
	void Start () {
        img = GetComponent<Image>();
        full_health = witch.GetComponent<witch>().health;
	}
	
	// Update is called once per frame
	void Update () {
        float curr_health = witch.GetComponent<witch>().health;
        float hi =  curr_health / full_health;
        img.fillAmount = curr_health / full_health;
    }
}