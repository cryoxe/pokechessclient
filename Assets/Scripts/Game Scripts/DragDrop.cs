using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;

    private void Awake(){
        canvas = GameObject.Find("Hand").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("Begin drag !");
    }
    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("End drag !");
    }
    public void OnDrag(PointerEventData eventData){
        Debug.Log("OnDrag !");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Clicked !");
    }
}
