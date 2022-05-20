using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartyMenu : MonoBehaviour
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
    private Initialisation myMenu;
    private SocketManager socketManager;
    private GameObject partyPrefab;

    void Start(){
        FindObjectOfType<SocketManager>().partyPlayerConnexionUpdateEvent += onMessagePlayerConnexion;
        FindObjectOfType<SocketManager>().partyStateUpdateEvent += onMessageState;
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        socketManager = myMenu.sceneManager.GetComponent<SocketManager>();
        partyPrefab = Resources.Load<GameObject>("Prefabs/PlayerInRoom");
    }

    public void MakeMyParty(Party myParty, bool isOwner = false)
    {
        ConnectToParty();
        partyComponent.nameOfRoom.text = myParty.name;
        Initialisation myMenu = FindObjectOfType<Initialisation>();
        //if(myMenu == null){ Debug.LogWarning("pas trouvé Initialisation...");}
        int idPlayer = 0;
        foreach(string player in myParty.players){
            idPlayer += 1;
            GameObject thisPlayer = Instantiate(partyPrefab, myMenu.PartyFitter.transform, false);
            thisPlayer.GetComponent<PlayerInPartyMenu>().MakeMyPlayer(player, idPlayer);
            thisPlayer.name = player; 
        }
        if(myParty.withPassword == false)
        {
            partyComponent.withPassword.SetActive(false);
        }
        else{partyComponent.withPassword.SetActive(true);}
        if(isOwner == true)
        {
            partyComponent.startButton.SetActive(true);
        }
        else{partyComponent.startButton.SetActive(false);}
        
    }

    public void ConnectToParty()
    {
        SubscribeForParty();
    }

    public void onMessagePlayerConnexion(PartyPlayerConnexionUpdate message)
    {
        Debug.Log(message);
        int idPlayer = 0;
        foreach(string player in message.players)
        {
            idPlayer += 1;
            GameObject thisPlayer = Instantiate(partyPrefab, myMenu.PartyFitter.transform, false);
            thisPlayer.GetComponent<PlayerInPartyMenu>().MakeMyPlayer(player, idPlayer);
            thisPlayer.name = player; 
        }

    }
    public void onMessageState(PartyStateUpdate message)
    {
        Debug.Log(message);
        if(message.state.ToLower() == "deleted")
        {
            myMenu.popUp.SendPopUp("Il semblerait que le propriétaire de cette partie se soit déconnecté...", true);
            myMenu.menuSwap.Transition(3);
        }
    }

    private async void SubscribeForParty()
    {
        var  partyPlayerConnexionUpdateDestination = new PartyPlayerConnexionUpdate{destination = "/parties/" + StaticVariable.nameOfThePartyIn + "/players/connection"};
        var partyStateUpdateDestination = new PartyStateUpdate{destination = "/parties/" + StaticVariable.nameOfThePartyIn + "/state"};
        await socketManager.SubscribeRequest(PartyPlayerConnexionUpdate.id, partyPlayerConnexionUpdateDestination.destination);
        await socketManager.SubscribeRequest(UpdatePartyMessage.id, partyStateUpdateDestination.destination);
    } 
}
