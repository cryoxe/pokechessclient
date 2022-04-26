using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateAccount : MonoBehaviour
{
    private Initialisation myMenu;

    public void Start()
    {
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
    }

    public void ShowVoletDroit()
    {
        myMenu.blank.enabled = true;
        GameObject.Find("UsernameInput").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("TrainerNameInput").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("PasswordInput").GetComponent<TMP_InputField>().text = "";

        //montrer le volet
        myMenu.voletAccount.SetTrigger("isShowing");
    }

    public void Create()
    {
        myMenu.sceneManager.GetComponent<RequestPOST>().SendPostRequestRegisterAccount("https://pokechess-card-game.herokuapp.com/api/v1/register", myMenu.usernameAccount, myMenu.passwordAccount, myMenu.trainerNameAccount);
    }

    public void Close()
    {
        myMenu.voletAccount.SetTrigger("isHidding");
        myMenu.blank.enabled = false;
    }
}
