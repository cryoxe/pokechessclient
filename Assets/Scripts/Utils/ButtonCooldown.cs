using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ButtonCooldown : MonoBehaviour
{
    private bool cooldown = false;
    private RectTransform rectTransform;
    private Vector2 OriginalPos;
    [Header("Bouton actif ?")]
    [Tooltip("est ce que le bouton est actif ?")]
    public bool isOn = true;
    [Space]

    [Header("OnClick :")]
    public UnityEvent FunctionOnClick;
    [Space]

    [Header("Autre :")]
    [Range(0.1f, 20)]
    [Tooltip("Temps d'inaction du bouton")]
    [SerializeField] public float timeOut = 5.0f;
    public List<string> buttonsToTurn;
    [Tooltip("le bouton doit-il se d�sactiv� onClick ?")]
    [SerializeField] bool autoTurnOff = false;
    [Space]
    [Header("Hover :")]
    [SerializeField] public TypeOfEnum typeOfEnum = TypeOfEnum.none;
    [SerializeField] private bool isHover;
    public enum TypeOfEnum
    {
        none,
        pushToRight,
        pushToLeft
    }
    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        OriginalPos = new Vector2(rectTransform.rect.x, rectTransform.rect.y);
    }

    private void Update()
    {
        if(gameObject.GetComponent<ButtonCooldown>().isOn == false)
        {
            gameObject.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f, 1f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    void OnMouseDown()
    {
        if (cooldown == false && isOn == true)
        {
            if(autoTurnOff == true)
            {
                Turn(buttonsToTurn, true);
            }
            Turn(buttonsToTurn);
            FunctionOnClick.Invoke();
            gameObject.GetComponent<Image>().color = new Color(0.65f,0.65f,0.65f,1f);
            if(autoTurnOff == true)
            {
                isOn = false;
            }
            else
            {
                Invoke("ResetCooldown", timeOut);
                isOn = false;
                cooldown = true;
            }
        }
    }
    void OnMouseOver()
    {
        if(typeOfEnum == TypeOfEnum.none)
        {
            return;
        }
        else if(typeOfEnum == TypeOfEnum.pushToRight && isHover == false)
        {
            isHover = true;
            LeanTween.moveLocalX(gameObject, -696, 0.2f);
            //print("Avance !");
        }
    }
    private void OnMouseExit()
    {
        if (typeOfEnum == TypeOfEnum.none)
        {
            return;
        }
        else if(typeOfEnum == TypeOfEnum.pushToRight && isHover == true)
        {
            isHover = false;            
            LeanTween.moveLocalX(gameObject, -796, 0.2f);
            //print("Recul !");
        }
    }

    void ResetCooldown()
    {
        isOn = true;
        cooldown = false;
        Turn(buttonsToTurn, true);
        gameObject.GetComponent<Image>().color = Color.white;
    }

    void Turn(List<string> buttons, bool state=false)
    {
        if(buttons == null)
        {
            return;
        }
        else
        {
            try
            {
                foreach(string s in buttons)
                {
                    GameObject.Find(s).GetComponent<ButtonCooldown>().isOn = state;
                }
            }
            catch
            {
                //Debug.LogWarning("bouton plus à l'affiche...");
                return;
            }

        }
  
    }

}
