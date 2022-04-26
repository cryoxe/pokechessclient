using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFilter : MonoBehaviour
{
    private Initialisation myMenu;
    [SerializeField]
    private bool isOnScreen = false;

    void Start(){
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();

    }
    void Update()
    {
        if(StaticVariable.isFilterEnable == true && isOnScreen == false){
            Debug.LogWarning("AFFICHER");
            LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(1700f, 344, 0), 0.3f);
            isOnScreen = true;
        }
    }

    public void DisableFilter()
    {
        myMenu.changePage.GoToPage(1);
        StaticVariable.isFilterEnable = false;
        LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(2020f, 344, 0), 0.3f);
        isOnScreen = false;
    }
}
