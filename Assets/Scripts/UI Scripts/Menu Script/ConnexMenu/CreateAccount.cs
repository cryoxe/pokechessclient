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
    public void Create()
    {
        myMenu.sceneManager.GetComponent<RequestPOST>().SendPostRequestRegisterAccount("https://pokechess-card-game.herokuapp.com/api/v1/register", myMenu.usernameAccount, myMenu.passwordAccount, myMenu.trainerNameAccount);
    }


    public void ShowVoletDroit()
    {
        GameObject.Find("UsernameInput").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("TrainerNameInput").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("PasswordInput").GetComponent<TMP_InputField>().text = "";

        //montrer le volet
        LeanTween.moveX(myMenu.voletAccount.gameObject.GetComponent<RectTransform>(), -322f, 0.2f);
    }

    public void Close()
    {
        myMenu.disableOnRequest.DisableAllInput(false);
        LeanTween.moveX(myMenu.voletAccount.gameObject.GetComponent<RectTransform>(), 322f, 0.2f).setOnComplete(NotHere);
    }
    private void NotHere()
    {
        myMenu.disableOnRequest.EnableAllInput(false);
    }

}
