using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePage : MonoBehaviour
{
    private Initialisation myMenu;
    private int goToThisPage;
    //public List<GameObject> rectTrans = new List<GameObject>();

    void Start(){
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
    }

    public void GoToPage(int page){
        if(page > StaticVariable.lastPage || page < 1){
            return;
        }
        else
        {
            goToThisPage = page;
            LeanTween.moveLocal(myMenu.Content, new Vector3(-1920, 0, 0), 5f * Time.deltaTime).setOnComplete(ManualTransition);
        }
    }

    void ManualTransition(){
        myMenu.clearCard.destroyAllCard();
        myMenu.Content.GetComponent<RectTransform>().position = new Vector3(1920, 0, 0);
        myMenu.requestGET.SendGetRequestCardPage(goToThisPage, 4);
        myMenu.pageNumber.text = "Page " + goToThisPage.ToString();        
        LeanTween.moveLocal(myMenu.Content, new Vector3(0, 0, 0), 5f * Time.deltaTime).setEaseInOutElastic();
    }

    public void NextPage(){
        if(StaticVariable.pageNumber >= StaticVariable.lastPage){
            return;
        }
        else{
            LeanTween.moveLocal(myMenu.Content, new Vector3(-1920, 0, 0), 5f * Time.deltaTime).setOnComplete(TransitionLeftToRight);         
        }


    }
    void TransitionLeftToRight(){
        int page = StaticVariable.pageNumber += 1;
        myMenu.clearCard.destroyAllCard();
        myMenu.Content.GetComponent<RectTransform>().position = new Vector3(1920, 0, 0);
        if(StaticVariable.isFilterEnable == true){
            myMenu.requestGET.SendGetRequestCardPage(page, 4, true, StaticVariable.filter);
        }
        else myMenu.requestGET.SendGetRequestCardPage(page, 4);

        myMenu.pageNumber.text = "Page " + page.ToString();        
        LeanTween.moveLocal(myMenu.Content, new Vector3(0, 0, 0), 5f * Time.deltaTime).setEaseInOutElastic();
    }

    public void PreviousPage(){
        if(StaticVariable.pageNumber <= 1){
            return;
        }
        else{
            LeanTween.moveLocal(myMenu.Content, new Vector3(1920, 0, 0), 5f * Time.deltaTime).setOnComplete(TransistionRightToLeft);    
        }


    }

    void TransistionRightToLeft(){
        int page = StaticVariable.pageNumber -= 1;
        myMenu.clearCard.destroyAllCard();
        myMenu.Content.GetComponent<RectTransform>().position = new Vector3(-1920, 0, 0);
        if(StaticVariable.isFilterEnable == true){
            myMenu.requestGET.SendGetRequestCardPage(page, 4, true, StaticVariable.filter);
        }
        else myMenu.requestGET.SendGetRequestCardPage(page, 4);
        myMenu.pageNumber.text = "Page " + page.ToString();
        LeanTween.moveLocal(myMenu.Content, new Vector3(0, 0, 0), 5f * Time.deltaTime).setEaseInOutElastic();
    }
}
