using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using SimpleJSON;
using System;

public class RequestGET : MonoBehaviour
{
    private Initialisation myMenu;
    private Animator Pokeball;

    // Start is called before the first frame update
    void Start()
    {
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        Pokeball = myMenu.pokeball;
    }
    public void SendGetRequestCardPage(int page = 1, int size = 4, bool isFilter = false, string filter = "")
    {
        StartCoroutine(GetRequestCardPage(page, size, isFilter, filter));
    }
    public void SendGetRequestAllParties()
    {
        StartCoroutine(GetRequestAllParties());
    }    

    IEnumerator GetRequestCardPage(int page, int size, bool isFilter, string filter){

        string CardPageURL = "";
        if (isFilter)
        {
            CardPageURL = StaticVariable.apiUrl + "pokemon?"+"page="+page+"&size="+size+"&name="+filter;
        }
        else
        {
            CardPageURL = StaticVariable.apiUrl + "pokemon?" + "page=" + page + "&size=" + size;
        }

        print(CardPageURL);
        using UnityWebRequest webRequest = UnityWebRequest.Get(CardPageURL);
        webRequest.SetRequestHeader("Authorization", StaticVariable.accessToken);
        // Request and wait for the desired page.
        myMenu.disableOnRequest.DisableAllInput();
        yield return webRequest.SendWebRequest();
        myMenu.disableOnRequest.EnableAllInput();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + webRequest.error);
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + webRequest.error);
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                //Debug.Log("Received: " + webRequest.responseCode);
                if(webRequest.responseCode == 204){
                    Debug.Log("No Card found !");
                    myMenu.popUp.SendPopUp("Aucune carte n'a été trouvée", true);
                    myMenu.resetFilter.DisableFilter();
                }
                else{
                    JSONNode pokemonPage = JSON.Parse(webRequest.downloadHandler.text);
                    JSONNode pokemonList = pokemonPage["items"];
                    StaticVariable.lastPage = pokemonPage["lastPage"];
                    Debug.LogWarning("LAST PAGE : " + StaticVariable.lastPage);
                    myMenu.cardPage.CreateCardInstanceForPage(pokemonList);                    
                }
                break;    
        }
    }
    IEnumerator GetRequestAllParties()
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(StaticVariable.apiUrl + "parties");
        webRequest.SetRequestHeader("Authorization", StaticVariable.accessToken);
        // Request and wait.
        myMenu.disableOnRequest.DisableAllInput();
        yield return webRequest.SendWebRequest();
        myMenu.disableOnRequest.EnableAllInput();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + webRequest.error);
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + webRequest.error);
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                //Debug.Log("Received: " + webRequest.responseCode);
                JSONNode listOfParties = JSON.Parse(webRequest.downloadHandler.text);
                JSONNode parties = listOfParties["parties"];
                GameObject partyPrefab = Resources.Load<GameObject>("Prefabs/Room");
                foreach(JSONNode party in parties){
                    GameObject thisRoom = Instantiate(partyPrefab, myMenu.LobbyFitter.transform, false);
                    thisRoom.GetComponent<RoomText>().MakeMyRoom(party["name"], party["owner"], party["withPassword"], party["numberOfPlayer"]);
                    thisRoom.name = party["name"];
                }                   
                break;    
        }
    }
}
