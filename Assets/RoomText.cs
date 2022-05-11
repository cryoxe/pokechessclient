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
        public TextMeshProUGUI accessType;
        public TextMeshProUGUI numberOfPlayer;
        public Image lockImage;


    }
    public RoomComponent roomComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void MakeMyRoom(string nameOfRoom, string nameOfOwner, bool isPassword=false, int numberOfPlayer=1)
    {
        roomComponent.nameOfRoom.text = nameOfRoom;
        roomComponent.nameOfOwner.text = nameOfOwner;
        roomComponent.numberOfPlayer.text = numberOfPlayer.ToString();
        if(isPassword == false)
        {
            //(80, 200, 40)
            roomComponent.accessType.text = "Public";
            roomComponent.accessType.color =  Color.green;
            roomComponent.lockImage.color =  Color.green;
        }
        else
        {
            //(200, 40, 40)
            roomComponent.accessType.text = "Privé";
            roomComponent.accessType.color =  Color.red;
            roomComponent.lockImage.color =  Color.red;
        }
    }
}
