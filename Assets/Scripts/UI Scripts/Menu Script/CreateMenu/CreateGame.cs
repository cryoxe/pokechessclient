using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateGame : MonoBehaviour
{
    private Initialisation myMenu;
    private GameObject PlayMenuCanvas;
    private GameObject CreategameCanvas;

    void Start()
    {
        myMenu = FindObjectOfType<Initialisation>();
        PlayMenuCanvas = myMenu.playButtonPlaceholder;
        CreategameCanvas = myMenu.createGameFields;
    }

    private void Hide(GameObject canvas, System.Action thenDo)
    {
        LeanTween.alphaCanvas(canvas.GetComponent<CanvasGroup>(), 0f, 0.3f).setOnComplete(thenDo);
    }
    private void Show(GameObject canvas, System.Action thenDo)
    {
        LeanTween.alphaCanvas(canvas.GetComponent<CanvasGroup>(), 1f, 0.3f).setOnComplete(thenDo);
    }

    public void TransitionToCreateGame(){
        CreategameCanvas.SetActive(true);
        myMenu.disableOnRequest.DisableAllInput(false);
        Hide(PlayMenuCanvas, ShowCreateGameFields);
    }
    private void ShowCreateGameFields()
    {
        PlayMenuCanvas.SetActive(false);
        Show(CreategameCanvas, EnableButtons);
    }

    public void TransitionToPlayMenu()
    {
        PlayMenuCanvas.SetActive(true);
        myMenu.disableOnRequest.DisableAllInput(false);
        Hide(CreategameCanvas, ShowPlayMenu);
    }    
    private void ShowPlayMenu()
    {
        CreategameCanvas.SetActive(false);
        Show(PlayMenuCanvas, EnableButtons);
    }

    private void EnableButtons(){myMenu.disableOnRequest.EnableAllInput(false);}


    public void CreateMyGame()
    {
        string _roomName = GameObject.Find("PartyNameInput").GetComponent<TMP_InputField>().text;
        string _password = GameObject.Find("PartyPasswordInput").GetComponent<TMP_InputField>().text;
        if(_password != ""){StaticVariable.passwordOfTheParty = _password;}
        myMenu.requestPOST.SendPostRequestCreateRoom(_roomName, _password);
    }
}
