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

    public void Connection()
    {
        myMenu.sceneManager.GetComponent<RequestPOST>().SendPostRequestAuthenticate("https://pokechess-card-game.herokuapp.com/api/v1/authenticate", myMenu.usernameConnexion, myMenu.passwordConnexion);
    }

    public void ShowVolet()
    {
        GameObject.Find("UsernameConnexionInput").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("PasswordConnexionInput").GetComponent<TMP_InputField>().text = "";
        //montrer le volet
        LeanTween.moveX(myMenu.voletConnexion.gameObject.GetComponent<RectTransform>(), -322f, 0.3f);
        
    }

    public void Close()
    {
        myMenu.disableOnRequest.DisableAllInput(false);
        LeanTween.moveX(myMenu.voletConnexion.gameObject.GetComponent<RectTransform>(), 322f, 0.3f).setOnComplete(NotHere);
    }

    private void NotHere()
    {
        myMenu.disableOnRequest.EnableAllInput(false);
    }

}
