using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchCard : MonoBehaviour
{
    private Initialisation myMenu;
    //private List<GameObject> rectTrans = new List<GameObject>();

    private string textOfSearch;

    // Start is called before the first frame update
    void Start()
    {
        myMenu = GetComponent<Initialisation>();
    }

    public void SearchForACard()
    {
        textOfSearch = GameObject.Find("SearchInput").GetComponent<TMP_InputField>().text;
        GameObject.Find("SearchInput").GetComponent<TMP_InputField>().text = "";
        textOfSearch = textOfSearch.Trim();
        StaticVariable.filter = textOfSearch;
        //Debug.Log("|" + textOfSearch + "|");
        if(textOfSearch == ""){
            return;
        }
        else{
            LeanTween.moveLocal(myMenu.Content, new Vector3(-1920, 0, 0), 5f * Time.deltaTime).setOnComplete(TransitionLeftToRight);
        }
        
    }

    void TransitionLeftToRight(){
        StaticVariable.isFilterEnable = true;
        print("StaticVariable.isFilterEnable = " + StaticVariable.isFilterEnable);
        int page = StaticVariable.pageNumber = 1;
        myMenu.clearCard.destroyAllCard();
        myMenu.Content.GetComponent<RectTransform>().position = new Vector3(1920, 0, 0);
        myMenu.requestGET.SendGetRequestCardPage(1, 4, true, textOfSearch);
        myMenu.pageNumber.text = "Page " + page.ToString();        
        LeanTween.moveLocal(myMenu.Content, new Vector3(0, 0, 0), 5f * Time.deltaTime).setEaseInOutElastic();
    }
}
