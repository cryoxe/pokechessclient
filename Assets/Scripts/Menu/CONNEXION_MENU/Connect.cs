using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Connect : MonoBehaviour
{
    private Initialisation myMenu;

    public void Start()
    {
        myMenu = GetComponent<Initialisation>();
    }


    public void ShowVolet()
    {
        myMenu.blank.enabled = true;
        GameObject.Find("UsernameConnexionInput").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("PasswordConnexionInput").GetComponent<TMP_InputField>().text = "";

        //montrer le volet
        myMenu.voletConnexion.SetTrigger("isShowing");
    }

    public void Connection()
    {
        myMenu.sceneManager.GetComponent<RequestPOST>().SendPostRequestAuthenticate("https://pokechess-card-game.herokuapp.com/api/v1/authenticate", myMenu.usernameConnexion, myMenu.passwordConnexion);
    }

    public void Close()
    {
        myMenu.voletConnexion.SetTrigger("isHidding");
    }
}
