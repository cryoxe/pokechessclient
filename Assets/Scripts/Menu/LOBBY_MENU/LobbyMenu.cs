using UnityEngine;
using System.Collections;

public class LobbyMenu : MonoBehaviour {

    private Initialisation myMenu;
    private SocketManager socketManager;

    private void Start()
    {
        FindObjectOfType<SocketManager>().websocketMessageEvent += onMessage;
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        socketManager = myMenu.sceneManager.GetComponent<SocketManager>();
    }

    public void ConnectToLobby()
    {
        SubscribeForLobby();
    }

    public void onMessage(string message)
    {
        Debug.Log(message);
    }

    private async void SubscribeForLobby()
    {
        await socketManager.SubscribeRequest(1, "/parties/creation");
        await socketManager.SubscribeRequest(2, "/parties/update");
        await socketManager.SubscribeRequest(3, "/parties/deletion");
    } 
}
