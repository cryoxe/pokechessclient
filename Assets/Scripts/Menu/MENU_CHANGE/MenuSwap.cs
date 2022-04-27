using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwap : MonoBehaviour
{
    private Initialisation myMenu;
    private RectTransform transitionUp, transitionDown;
    private RequestGET requestGET;

    private void Start()
    {
        myMenu = GetComponent<Initialisation>();
        transitionUp = GameObject.Find("TransitionUp").GetComponent<RectTransform>();
        transitionDown = GameObject.Find("TransitionDown").GetComponent<RectTransform>();
        requestGET = GameObject.Find("SceneManager").GetComponent<RequestGET>();
    }

    public void Transition(int idMenu)
    {
        StaticVariable.idForMenuSwitch = idMenu;
        LeanTween.size(transitionUp, new Vector2(0f, 560f), 0.4f);
        LeanTween.size(transitionDown, new Vector2(0f, 560f), 0.4f).setOnComplete(FinishTransition);
    }

    void Switch()
    {
        switch(StaticVariable.idForMenuSwitch){
            case 0 :
                Debug.Log("Called for ConnexionMenu!");
                SwitchToConnexionMenu();
                break;
            case 1 :
                Debug.Log("Called for MainMenu!");
                SwitchToMainMenu();
                break;
            case 2 :
                Debug.Log("Called for Library!");
                SwitchToCardLibrary();
                break;
            default :
                Debug.LogError("L'ID de menu n'a pas été reconnu !");
                break;
        }
    }



    public void SwitchToConnexionMenu()
    {
        myMenu.accountPlaceholder.SetActive(true);

        myMenu.mainButtonPlaceholder.SetActive(false);
        myMenu.playButtonPlaceholder.SetActive(false);
        myMenu.CardLibrary.SetActive(false);
    }
    void SwitchToMainMenu()
    {
        myMenu.mainButtonPlaceholder.SetActive(true);
        myMenu.buttonSlide.SetTrigger("Show");
        myMenu.clearCard.destroyAllCard();
        myMenu.accountPlaceholder.SetActive(false);
        myMenu.playButtonPlaceholder.SetActive(false);
        myMenu.CardLibrary.SetActive(false);
        

    }
    void SwitchToCardLibrary()
    {
        myMenu.CardLibrary.SetActive(true);
        requestGET.SendGetRequestCardPage(1, 4);
        StaticVariable.currentPage = 1;
        StaticVariable.pageNumber = 1;
        myMenu.pageNumber.text = "Page 1";
        myMenu.buttonSlide.SetTrigger("Hide");
        myMenu.mainButtonPlaceholder.SetActive(false);
        myMenu.playButtonPlaceholder.SetActive(false);
    }
    void SwitchToPlayMenu(){
        myMenu.accountPlaceholder.SetActive(false);
        myMenu.mainButtonPlaceholder.SetActive(false);
        myMenu.playButtonPlaceholder.SetActive(false);
    }

    void FinishTransition()
    {
        Switch();
        LeanTween.size(transitionUp, new Vector2(0f, 25f), 0.4f);
        LeanTween.size(transitionDown, new Vector2(0f, 25f), 0.4f);
    }
}
