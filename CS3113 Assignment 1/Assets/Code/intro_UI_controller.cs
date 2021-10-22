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
    public Transform final_pos;
    private float Player_trans;
    private float stage = 0;
    void Start()
    {
        
        Player_trans = gameObject.GetComponent<Transform>().position.x;
        move_instruction.SetActive(true);
        earth_instruction.SetActive(false);
        light_instruction.SetActive(false);
        attack_instruction.SetActive(false);
        print(stage);

    }

    void Update()
    {
        Player_trans = gameObject.GetComponent<Transform>().position.x;
        if(stage == 0){
            if(Player_trans>=master_earth.position.x){
                move_instruction.SetActive(false);
                earth_instruction.SetActive(true);
                stage=1;
                print(stage);
            }
        }
        else if (stage == 1){
            if(Player_trans>=master_light.position.x){
                earth_instruction.SetActive(false);
                light_instruction.SetActive(true);
                stage=2;
                print(stage);
            }
        }
        else if (stage == 2){
            if(Player_trans>=master_attack.position.x){
                light_instruction.SetActive(false);
                attack_instruction.SetActive(true);
                stage= 3;
                print(stage);
            }
        }
        else if (stage == 3){
            if(Player_trans>=final_pos.position.x){
                light_instruction.SetActive(false);
                attack_instruction.SetActive(true);
                stage= 4;
                print(stage);
            }
        }
        else if (stage == 4){
                move_instruction.SetActive(true);
                attack_instruction.SetActive(false);
                stage=5;
                print(stage);
            
        }
        
    }
}
