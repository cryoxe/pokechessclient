using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using SimpleJSON;
using System;

public class RequestPOST : MonoBehaviour
{
    private Initialisation myMenu;
    private SocketManager socketManager;

    private CreateAccount createAccount;
    private Connect connect;

    private Animator Pokeball;

    private PopUp popUp;

    private void Start()
    {
        myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        Pokeball = myMenu.pokeball;
        socketManager = myMenu.sceneManager.GetComponent<SocketManager>();

        createAccount = myMenu.sceneManager.GetComponent<CreateAccount>();
        connect = myMenu.sceneManager.GetComponent<Connect>();

        popUp = GameObject.Find("SceneManager").GetComponent<PopUp>();

        //popUpClose = myMenu.popUpClose;
        //popUpAnimation = myMenu.popUpAnimation;
        //popUpMessage = myMenu.popUpMessage;
    }


    public UnityWebRequest CreateJsonToSend(object toJson, string uri)
    {
        string json = JsonConvert.SerializeObject(toJson);
        print("Je POST : " + json);

        var request = new UnityWebRequest(uri, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

    //Fonction public => StartCoroutine()
    public void SendPostRequestRegisterAccount(string url, string _username, string _password, string _trainerName) {
        StartCoroutine(PostRequestRegisterAccount(url, _username, _password, _trainerName));
    }
    
    public void SendPostRequestAuthenticate(string url, string _login, string _password) {
        StartCoroutine(PostRequestAuthenticate(url, _login, _password));
    }

    public void SendPostRequestCreateRoom(string url, TextMeshProUGUI outPutAera, string _roomName, string _password)
    {
        StartCoroutine(PostRequestCreateRoom(url, outPutAera, _roomName, _password));
    }

    //coroutine d'envoi de POST
   IEnumerator PostRequestRegisterAccount(string url, string _username, string _password, string _trainerName)
   {
        //créer le JSON à envoyer
        var user = new Classes.UserAccount
        {
            username = _username,
            password = _password,
            trainerName = _trainerName
        };
        UnityWebRequest request = CreateJsonToSend(user, url);

        myMenu.disableOnRequest.DisableAllInput();
        popUp.SendPopUp("Chargement...", false);
        yield return request.SendWebRequest();
        myMenu.disableOnRequest.EnableAllInput();

        if (request.result == UnityWebRequest.Result.Success)
        {
            popUp.ChangePopUpMessage("Compte créé avec succès !\nVous pouvez maintenant vous connecter. ");
            popUp.PopUpShowInteraction();
            createAccount.Close();
        }
        else 
        {
            JSONNode jsonData = JSON.Parse(request.downloadHandler.text);

            if (jsonData == null)
            {
                print("_______________NO DATA_______________");
                popUp.ChangePopUpMessage("Le serveur n'as pas envoyé de données...");
                popUp.PopUpShowInteraction();
            }
            else
            {
                print("_______________DATA_______________");
                if(request.result == UnityWebRequest.Result.ProtocolError)
                {
                    print("Je reçois : " + request.downloadHandler.text);

                    if (int.TryParse(jsonData[1], out int resultat))
                    {
                        print("il y a un status => Conflict");
                        popUp.ChangePopUpMessage("Il existe déjà un compte avec ce nom.");
                        popUp.PopUpShowInteraction();
                    }
                    else
                    {
                        print("il n'y a pas de status => FieldEmpty");
                        popUp.ChangePopUpMessage("Vous ne pouvez pas laisser de champ vide.");
                        popUp.PopUpShowInteraction();
                    }
                }
                else if(request.result == UnityWebRequest.Result.ConnectionError)
                {
                    popUp.ChangePopUpMessage("Impossible de se connecter.");
                    popUp.PopUpShowInteraction();
                }
            }

        }

   }

   IEnumerator PostRequestAuthenticate(string url, string _login, string _password)
   {
        //créer le JSON à envoyer
        var user = new Classes.JwtRequest
        {
            username = _login,
            password = _password
        };
        UnityWebRequest request = CreateJsonToSend(user, url);

        myMenu.disableOnRequest.DisableAllInput();
        popUp.SendPopUp("Chargement...", false);
        yield return request.SendWebRequest();
        myMenu.disableOnRequest.EnableAllInput();

        if (request.result == UnityWebRequest.Result.Success)
        {
            //Backend
            Debug.Log("Success");
            Debug.Log(Encoding.UTF8.GetString(request.downloadHandler.data));
            Classes.JwtResponse response = JsonConvert.DeserializeObject<Classes.JwtResponse>(Encoding.UTF8.GetString(request.downloadHandler.data));
            StaticVariable.accessToken = "Bearer " + response.access_token;
            StaticVariable.refreshToken = "Bearer " + response.refresh_token;
            socketManager.ConnectWebsocket();

            //Visuel
            popUp.ChangePopUpMessage("Connexion réussi !");
            myMenu.blur.enabled = false;
            popUp.ClosePopUp();
            connect.Close();
            myMenu.menuSwap.Transition(1);
        }
        else
        {
            JSONNode jsonData = JSON.Parse(request.downloadHandler.text);

            if (jsonData == null)
            {
                print("_______________NO DATA_______________");
                popUp.ChangePopUpMessage("Le serveur n'as pas envoyé de données...");
                popUp.PopUpShowInteraction();
            }
            else
            {
                print("_______________DATA_______________");
                if (request.result == UnityWebRequest.Result.ProtocolError)
                {
                    print("Je reçois : " + request.downloadHandler.text);

                    popUp.ChangePopUpMessage("Ce compte ne semble pas exister...");
                    popUp.PopUpShowInteraction();

                }
                else if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    popUp.ChangePopUpMessage("Impossible de se connecter.");
                    popUp.PopUpShowInteraction();
                }
            }

        }

    }

   IEnumerator PostRequestCreateRoom(string url, TextMeshProUGUI outPutAera, string _roomName, string _password) 
   {
        //créer le JSON à envoyer
        var user = new Classes.NewRoom
        {
            name = _roomName,
            password = _password
        };
        string json = JsonConvert.SerializeObject(user);
        print("Je POST : " + json);

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", StaticVariable.accessToken);//"Content-Type", "application/json", 
        request.SetRequestHeader("Content-Type", "application/json");

        //envoyer la requête
        outPutAera.text = "Chargement...";
        myMenu.disableOnRequest.DisableAllInput();
        yield return request.SendWebRequest();
        myMenu.disableOnRequest.EnableAllInput();

        //résultat :
        if (request.result == UnityWebRequest.Result.Success)
        {
            outPutAera.text = "Succès";
        }
        else
        {
            JSONNode jsonData = JSON.Parse(request.downloadHandler.text);

            if (jsonData == null)
            {
                print("_______________NO DATA_______________");
                outPutAera.text = "Le serveur n'as pas envoyé de données...";
            }
            else
            {
                print("_______________DATA_______________");
                if (request.result == UnityWebRequest.Result.ProtocolError)
                {
                    print("Impossible de créer la salle...");
                }
                else if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    outPutAera.text = "Impossible de se connecter.";
                }
            }

        }

   }
}
