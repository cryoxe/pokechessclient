using UnityEngine;
using System.Collections;

public class LobbyMenu : MonoBehaviour {

    private Initialisation myMenu;
    private SocketManager socketManager;
    private GameObject partyPrefab;

    private void Start()
    {
        FindObjectOfType<SocketManager>().creationPartyMessageEvent += onMessage;
        FindObjectOfType<SocketManager>().deletionPartyMessageEvent += test;
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
        thisRoom.name = message.name;
        Debug.Log("Creating a paty with name : " + thisRoom.name);
        thisRoom.GetComponent<RoomText>().MakeMyRoom(message.name, message.owner, message.withPassword);
        
    }

    public void test(DeletionPartyMessage message)
    {
        Debug.Log(message);
        Debug.Log("Party to destroy : " + message.name);
        Debug.Log("Trying to destroy a room...");
        try
        {
            Destroy(GameObject.Find(message.name));
        }
        catch
        {
            Debug.LogWarning("la salle à détruire n'a pas été trouvé...");
        }
        
    }

    private async void SubscribeForLobby()
    {
        await socketManager.SubscribeRequest(CreationPartyMessage.id, CreationPartyMessage.destination);
        await socketManager.SubscribeRequest(UpdatePartyMessage.id, UpdatePartyMessage.destination);
        await socketManager.SubscribeRequest(DeletionPartyMessage.id, DeletionPartyMessage.destination);
    } 
}
