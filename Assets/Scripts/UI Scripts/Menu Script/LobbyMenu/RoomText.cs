using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomText : MonoBehaviour
{
    [System.Serializable]
    public struct RoomComponent
    {
        public TextMeshProUGUI nameOfRoom;
        public TextMeshProUGUI nameOfOwner;
        public TextMeshProUGUI numberOfPlayer;
        public Image lockImage;


    }
    public RoomComponent roomComponent;
    private bool isPasswordInThisRoom;
    private Initialisation myMenu;
    private RectTransform rect;
    private GameObject close;
    public BoxCollider2D[] boxCollider2Ds;
    private TMP_InputField inputPassword;

    private void Start()
    {
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
    }

    public void MakeMyRoom(string nameOfRoom, string nameOfOwner="Mssigno", bool isPassword=false, int numberOfPlayer=1)
    {
        roomComponent.nameOfRoom.text = nameOfRoom;
        roomComponent.nameOfOwner.text = nameOfOwner;
        roomComponent.numberOfPlayer.text = numberOfPlayer.ToString();
        isPasswordInThisRoom = isPassword;
        if(isPassword == false)
        {
            //(80, 200, 40)
            roomComponent.lockImage.color =  Color.green;
        }
        else
        {
            //(200, 40, 40)
            roomComponent.lockImage.color =  Color.red;
        }
    }

    public void JoinThisRoom()
    {
        string password = "";
        int numberOfPlayer = int.Parse(roomComponent.numberOfPlayer.text);
        if(numberOfPlayer >=8){myMenu.popUp.SendPopUp("La partie est déjà pleine", true);}
        else
        {
            if(isPasswordInThisRoom == true)
            {
                GameObject poPupPrefab = Resources.Load<GameObject>("Prefabs/PopUp");
                GameObject newPopUp = Instantiate(poPupPrefab, GameObject.Find("PopUpCanvas").GetComponent<RectTransform>(), false);
                boxCollider2Ds = GameObject.Find("Canvases").GetComponentsInChildren<BoxCollider2D>(false);

                newPopUp.GetComponentInChildren<TextMeshProUGUI>().text = "Cette salle est privé, veuillez donner le mot de passe :";
                newPopUp.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
                rect = newPopUp.GetComponent<RectTransform>();
                close = GameObject.Find("Close");
                close.GetComponent<Button>().enabled = true;
                close.GetComponent<Button>().onClick.AddListener(SendPassword);
                LeanTween.scale(rect, new Vector3(1f, 1f, 1f), 0.45f).setEaseOutBounce();
            }
            else
            {
                password = "";
                FindObjectOfType<RequestPOST>().SendPostRequestJoinRoom(roomComponent.nameOfRoom.text, password);
            }
        }
    }

    private void SendPassword()
    {   
        inputPassword = GameObject.Find("InputFieldForRoomPassword").GetComponent<TMP_InputField>();
        FindObjectOfType<RequestPOST>().SendPostRequestJoinRoom(roomComponent.nameOfRoom.text,inputPassword.text);
        rect = GameObject.Find("PopUp(Clone)").GetComponent<RectTransform>();
        foreach (BoxCollider2D b in boxCollider2Ds)
        {
            b.enabled = true;
        }
        myMenu.DisableBlur();        
        Destroy(rect.gameObject);
        //LeanTween.scale(rect, new Vector3(0f, 0f, 0f), 0.45f).setDestroyOnComplete(true);

    }
}
