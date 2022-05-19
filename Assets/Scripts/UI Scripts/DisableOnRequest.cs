using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableOnRequest : MonoBehaviour
{
    private Initialisation myMenu;
    [SerializeField]
    public ButtonCooldown[] buttonCooldowns;
    [SerializeField]
    public Button[] buttons;

    void Start(){
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
    }

    public void DisableAllInput(bool pokeball = true){
        if(pokeball == true){
            if(myMenu.pokeball.GetCurrentAnimatorStateInfo(0).IsName("Rotate")){
                myMenu.pokeball.SetTrigger("stop");
                //print("LA POKEBALL S'ARRÊTE !"); 
            }
            else{
                myMenu.pokeball.SetTrigger("isRotating");
                //print("LA POKEBALL TOURNE");            
            }     
        }
        buttons = GetComponentsInChildren<Button>(true);
        buttonCooldowns = GetComponentsInChildren<ButtonCooldown>(true);
        foreach(Button b in buttons){
            b.enabled = false;
            //Debug.Log(b);
        }  
        foreach(ButtonCooldown bc in buttonCooldowns){
            bc.isOn = false;
            bc.enabled = false;
            //Debug.Log(bc);           
        } 
        Array.Clear(buttonCooldowns, 0, buttonCooldowns.Length);
        Array.Clear(buttons, 0, buttons.Length);
    }
    public void EnableAllInput(bool pokeball = true){
        if(pokeball == true){
            myMenu.pokeball.SetTrigger("stop");
            print("LA POKEBALL S'ARRÊTE !"); 
        }
        Button[] buttons = GetComponentsInChildren<Button>(true);
        ButtonCooldown[] buttonCooldowns = GetComponentsInChildren<ButtonCooldown>(true);
        foreach(Button b in buttons){
            b.enabled = true;
        }
        foreach(ButtonCooldown bc in buttonCooldowns){
            bc.isOn = true;
            bc.enabled = true;
        }

        Array.Clear(buttonCooldowns, 0, buttonCooldowns.Length);
        Array.Clear(buttons, 0, buttons.Length);

    }
}
