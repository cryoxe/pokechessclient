using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    private Initialisation myMenu;
    private RectTransform rect;
    private GameObject close;
    private GameObject inputPassword;
    public BoxCollider2D[] boxCollider2Ds;

    private void Start()
    {
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
    }

    public void SendPopUp(string theMessage, bool interaction)
    {
        GameObject poPupPrefab = Resources.Load<GameObject>("Prefabs/PopUp");
        GameObject newPopUp = Instantiate(poPupPrefab, GameObject.Find("PopUpCanvas").GetComponent<RectTransform>(), false);
        boxCollider2Ds = GameObject.Find("Canvases").GetComponentsInChildren<BoxCollider2D>(false);

        newPopUp.GetComponentInChildren<TextMeshProUGUI>().text = theMessage;
        newPopUp.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
        rect = newPopUp.GetComponent<RectTransform>();
        close = GameObject.Find("Close");
        close.GetComponent<Button>().enabled = true;
        inputPassword = GameObject.Find("InputFieldForRoomPassword");
        inputPassword.SetActive(false);
        if (interaction == false)
        {
            close.SetActive(false);
        }
        close.GetComponent<Button>().onClick.AddListener(ClosePopUp);
        LeanTween.scale(rect, new Vector3(1f, 1f, 1f), 0.45f).setEaseOutBounce();
        //print("APPARU");
        foreach(BoxCollider2D b in boxCollider2Ds)
        {
            b.enabled = false;
        }
        myMenu.EnableBlur();
    }
    public void ClosePopUp()
    {
        myMenu.pokeball.SetTrigger("stop");
        rect = GameObject.Find("PopUp(Clone)").GetComponent<RectTransform>();
        LeanTween.scale(rect, new Vector3(0f, 0f, 0f), 0.45f).setDestroyOnComplete(true);
        //print("DISPARU");
        foreach (BoxCollider2D b in boxCollider2Ds)
        {
            b.enabled = true;
        }
        myMenu.DisableBlur();
    }

    public void PopUpShowInteraction()
    {
        close.SetActive(true);
        close.GetComponent<Button>().onClick.AddListener(ClosePopUp);
    }
    public void ChangePopUpMessage(string theMessage)
    {
        GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = theMessage;
    }
    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
