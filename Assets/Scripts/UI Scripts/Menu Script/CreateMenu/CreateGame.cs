using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateGame : MonoBehaviour
{
    private Initialisation myMenu;
    private GameObject PlayMenuCanvas;

    void Start()
    {
        myMenu = FindObjectOfType<Initialisation>();
        PlayMenuCanvas = GameObject.Find("PlayButtonPlaceholder");
    }

    public void TransitionToCreateGame(){
        myMenu.CreateGamePlaceholder.SetActive(true);
        myMenu.disableOnRequest.DisableAllInput(false);
        LeanTween.alphaCanvas(PlayMenuCanvas.GetComponent<CanvasGroup>(), 0f, 0.3f).setOnComplete(ShowCreateGameFields);
    }
    private void ShowCreateGameFields()
    {
        PlayMenuCanvas.SetActive(false);
        LeanTween.alphaCanvas(myMenu.CreateGamePlaceholder.GetComponent<CanvasGroup>(), 1f, 0.3f).setOnComplete(EnableButtons);
    }

    public void TransitionToPlayMenu()
    {
        myMenu.disableOnRequest.DisableAllInput(false);
        LeanTween.alphaCanvas(myMenu.CreateGamePlaceholder.GetComponent<CanvasGroup>(), 0f, 0.3f).setOnComplete(ShowPlayMenu);

    }    
    private void ShowPlayMenu()
    {
        PlayMenuCanvas.SetActive(true);
        LeanTween.alphaCanvas(PlayMenuCanvas.GetComponent<CanvasGroup>(), 1f, 0.3f).setOnComplete(EnableButtons);
    }

    private void EnableButtons(){myMenu.disableOnRequest.EnableAllInput(false);}


    public void CreateMyGame()
    {
        string _roomName = GameObject.Find("PartyNameInput").GetComponent<TMP_InputField>().text;
        string _password = GameObject.Find("PartyPasswordInput").GetComponent<TMP_InputField>().text;
        myMenu.requestPOST.SendPostRequestCreateRoom(_roomName, _password);
    }
}
