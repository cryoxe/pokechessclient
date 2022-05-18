using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using SimpleJSON;
using System;
using UnityEngine.UI;

public class RequestDELETE : MonoBehaviour
{
    private Initialisation myMenu;
    private TextMeshProUGUI outPutAera;
    //private Animator Pokeball;

    private void Start()
    {
        myMenu = FindObjectOfType<Initialisation>();
    }

    public void SendDeleteRequestRoom()
    {
        StartCoroutine(DeleteRequestRoom());
    }

    IEnumerator DeleteRequestRoom()
    {
        var request = new UnityWebRequest(StaticVariable.apiUrl+"parties", "DELETE");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", StaticVariable.accessToken);

        myMenu.disableOnRequest.DisableAllInput();
        yield return request.SendWebRequest();
        myMenu.disableOnRequest.EnableAllInput();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("L'utilsateur a bien quitté la partie");
            myMenu.menuSwap.Transition(3);
        }
        else
        {
            Debug.Log("échec de quitter la salle");
        }
    }
}
