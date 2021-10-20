using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro_UI_controller : MonoBehaviour
{
    public GameObject move_instruction;
    public GameObject earth_instruction;
    public GameObject light_instruction;
    public GameObject attack_instruction;

    public Transform master_earth;
    public Transform master_light;
    public Transform master_attack;
    private float Player_trans;
    private int stage = 0;
    void Start()
    {
        
        Player_trans = gameObject.GetComponent<Transform>().position.x;
        move_instruction.SetActive(true);
        earth_instruction.SetActive(false);
        light_instruction.SetActive(false);
        attack_instruction.SetActive(false);

    }

    void Update()
    {
        Player_trans = gameObject.GetComponent<Transform>().position.x;
        if(stage == 0){
            if(Player_trans>=master_earth.position.x){
                move_instruction.SetActive(false);
                earth_instruction.SetActive(true);
                stage+=1;
            }
        }
        if (stage == 1){
            if(Player_trans>=master_light.position.x){
                earth_instruction.SetActive(false);
                light_instruction.SetActive(true);
                stage+=1;
            }
        }
        if (stage == 2){
            if(Player_trans>=master_attack.position.x){
                light_instruction.SetActive(false);
                attack_instruction.SetActive(true);
                stage+=1;
            }
        }
    }
}
