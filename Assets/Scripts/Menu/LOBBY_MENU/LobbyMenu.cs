using UnityEngine;
using System.Collections;

public class LobbyMenu : MonoBehaviour {

    private Initialisation myMenu;
    private SocketManager socketManager;
    private GameObject partyPrefab;

    private void Start()
    {
        FindObjectOfType<SocketManager>().creationPartyMessageEvent += onMessage;
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        socketManager = myMenu.sceneManager.GetComponent<SocketManager>();
        partyPrefab = Resources.Load<GameObject>("Prefabs/Room");
    }

    public void ConnectToLobby()
    {
        SubscribeForLobby();
    }

    public void onMessage(CreationPartyMessage message)
    {
        Debug.Log(message);
        GameObject thisRoom = Instantiate(partyPrefab, myMenu.LobbyFitter.transform, false);
        thisRoom.GetComponent<RoomText>().MakeMyRoom(message.name, message.owner, message.withPassword);
        
    }

    private async void SubscribeForLobby()
    {
        await socketManager.SubscribeRequest(CreationPartyMessage.id, CreationPartyMessage.destination);
        await socketManager.SubscribeRequest(UpdatePartyMessage.id, UpdatePartyMessage.destination);
        await socketManager.SubscribeRequest(DeletionPartyMessage.id, DeletionPartyMessage.destination);
    } 
}
