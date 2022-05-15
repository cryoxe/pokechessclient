using UnityEngine;
using System.Collections;

public class LobbyMenu : MonoBehaviour {

    private Initialisation myMenu;
    private SocketManager socketManager;
    private GameObject partyPrefab;

    private void Start()
    {
        FindObjectOfType<SocketManager>().creationPartyMessageEvent += onMessageCreation;
        FindObjectOfType<SocketManager>().deletionPartyMessageEvent += onMessageDeletion;
        FindObjectOfType<SocketManager>().updatePartyMessageEvent += onMessageUpdate;
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        socketManager = myMenu.sceneManager.GetComponent<SocketManager>();
        partyPrefab = Resources.Load<GameObject>("Prefabs/Room");
    }

    public void ConnectToLobby()
    {
        SubscribeForLobby();
    }

    public void onMessageCreation(CreationPartyMessage message)
    {
        Debug.Log(message);
        GameObject thisRoom = Instantiate(partyPrefab, myMenu.LobbyFitter.transform, false);
        thisRoom.name = message.name;
        Debug.Log("Creating a paty with name : " + thisRoom.name);
        thisRoom.GetComponent<RoomText>().MakeMyRoom(message.name, message.owner, message.withPassword);
        
    }

    public void onMessageDeletion(DeletionPartyMessage message)
    {
        Debug.Log(message);
        Debug.Log("Party to destroy : " + message.name);
        Debug.Log("Trying to destroy a room...");
        try
        {
            Destroy(GameObject.Find(message.name));
            Debug.LogWarning("la salle a bien été détruite");
        }
        catch
        {
            Debug.LogWarning("la salle à détruire n'a pas été trouvé...");
        }
        
    }

    public void onMessageUpdate(UpdatePartyMessage message)
    {
        Debug.Log(message);
        GameObject roomToUpdate = GameObject.Find(message.name);
        roomToUpdate.GetComponent<RoomText>().roomComponent.numberOfPlayer.text = message.playerNumber.ToString();
    }

    private async void SubscribeForLobby()
    {
        await socketManager.SubscribeRequest(CreationPartyMessage.id, CreationPartyMessage.destination);
        await socketManager.SubscribeRequest(UpdatePartyMessage.id, UpdatePartyMessage.destination);
        await socketManager.SubscribeRequest(DeletionPartyMessage.id, DeletionPartyMessage.destination);
    } 
}
