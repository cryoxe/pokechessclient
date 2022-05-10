using UnityEngine;
using System.Collections;

public class LobbyMenu : MonoBehaviour {

    private Initialisation myMenu;
    private SocketManager socketManager;

    private void Start()
    {
        FindObjectOfType<SocketManager>().creationPartyMessageEvent += onMessage;
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        socketManager = myMenu.sceneManager.GetComponent<SocketManager>();
    }

    public void ConnectToLobby()
    {
        SubscribeForLobby();
    }

    public void onMessage(CreationPartyMessage message)
    {
        Debug.Log(message);
    }

    private async void SubscribeForLobby()
    {
        await socketManager.SubscribeRequest(CreationPartyMessage.id, CreationPartyMessage.destination);
        await socketManager.SubscribeRequest(UpdatePartyMessage.id, UpdatePartyMessage.destination);
        await socketManager.SubscribeRequest(DeletionPartyMessage.id, DeletionPartyMessage.destination);
    } 
}
