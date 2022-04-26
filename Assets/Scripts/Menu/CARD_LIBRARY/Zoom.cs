using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    private Button zoomButton;
    public bool isZooming = false;
    private Initialisation myMenu;
    RectTransform rectObject;
    Vector3 oldPosition;

    public int index;

    private void Start()
    {
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        zoomButton = GetComponent<Button>();
        zoomButton.onClick.AddListener(Zooming);
        rectObject = GetComponent<RectTransform>();
        oldPosition = rectObject.localPosition;
        index = transform.GetSiblingIndex();
    }

    public void Zooming()
    {
        if (isZooming == false)
        {
            myMenu.Content.GetComponent<HorizontalLayoutGroup>().enabled = false;
            gameObject.transform.SetParent(myMenu.noBlur);
            SetLayerRecursively(gameObject, 6);
            myMenu.EnableBlur();
            myMenu.disableOnRequest.DisableAllInput(false);
            gameObject.GetComponent<Button>().enabled = true;

            rectObject.localPosition = new Vector3(rectObject.localPosition.x, rectObject.localPosition.y, 1); 
            LeanTween.moveLocal(gameObject, new Vector3(0f, 0f, 0f), 0.10f);
            LeanTween.scale(rectObject, new Vector3(0.75f, 0.75f, 0.75f), 0.15f);
            isZooming = true;
        }
        else
        {
            myMenu.Content.GetComponent<HorizontalLayoutGroup>().enabled = true;
            gameObject.transform.SetParent(myMenu.Content.transform);
            SetLayerRecursively(gameObject, 0);
            myMenu.DisableBlur();
            myMenu.disableOnRequest.EnableAllInput(false);

            rectObject.localPosition = new Vector3(rectObject.localPosition.x, rectObject.localPosition.y, 0); 
            LeanTween.scale(rectObject, new Vector3(0.5f, 0.5f, 0.1f), 0f);
            LeanTween.moveLocal(gameObject, oldPosition, 0.0f).setOnComplete(updateLayout);
            transform.SetSiblingIndex(index);
            isZooming = false;
        }
    }

    void updateLayout(){
        myMenu.Content.GetComponent<HorizontalLayoutGroup>().enabled = false;
        myMenu.Content.GetComponent<HorizontalLayoutGroup>().enabled = true;
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            Debug.Log("EMPTY GAME OBJECT");
            return;
        }
       
        obj.layer = newLayer;
       
        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}
