using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartyText : MonoBehaviour
{
    [System.Serializable]
    public struct PartyComponent
    {
        public TextMeshProUGUI nameOfRoom;
        public GameObject fitter;
        public GameObject withPassword;
        public GameObject startButton;

    }
    public PartyComponent partyComponent;

    public void MakeMyParty(Party myParty)
    {
        partyComponent.nameOfRoom.text = myParty.name;
        GameObject partyPrefab = Resources.Load<GameObject>("Prefabs/PlayerInRoom");
        if(partyPrefab == null){ Debug.LogWarning("pas trouvé Prefab...");}
        Initialisation myMenu = FindObjectOfType<Initialisation>();
        if(myMenu == null){ Debug.LogWarning("pas trouvé Initialisation...");}
        int idPlayer = 0;
        foreach(string player in myParty.players){
            idPlayer += 1;
            GameObject thisPlayer = Instantiate(partyPrefab, myMenu.PartyFitter.transform, false);
            thisPlayer.GetComponent<PlayerInPartyText>().MakeMyPlayer(player, idPlayer);
            thisPlayer.name = player; 
        }
        if(myParty.withPassword == false)
        {
            partyComponent.withPassword.SetActive(false);
        }
        else{partyComponent.withPassword.SetActive(true);}

        partyComponent.startButton.SetActive(false);

    }
}
