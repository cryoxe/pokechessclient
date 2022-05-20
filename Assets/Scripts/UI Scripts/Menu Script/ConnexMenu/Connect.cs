using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Connect : MonoBehaviour
{
    private Initialisation myMenu;

    public void Start()
    {
        myMenu = GetComponent<Initialisation>();
    }

    public void Connection()
    {
        JwtRequest playerData = new JwtRequest();
        playerData.username = myMenu.usernameConnexion;
        playerData.password = myMenu.passwordConnexion;
        myMenu.sceneManager.GetComponent<RequestPOST>().SendPostRequestAuthenticate(playerData);
    }

    public void ShowVolet()
    {
        //Vérifier si il existe une sauvegarde

        if(File.Exists(Application.persistentDataPath + "/RefreshToken.json"))
        {
            //OUI --> Utiliser ce fichier
            Debug.Log("Fichier trouvé à : " + Application.persistentDataPath);
            string path = Application.persistentDataPath + "/RefreshToken.json";
            StreamReader reader = new StreamReader(path); 
            //Lire le fichier
            RefreshToken playerData = JsonUtility.FromJson<RefreshToken>(reader.ReadToEnd());
            reader.Close();
            //afficher le nom du joueur stocké dans le fichier
            StaticVariable.theUsername = playerData.username;
            myMenu.connectedPlayerName.text = StaticVariable.theUsername;
            //envoyer une requête pour obtenir le token d'access
            myMenu.requestGET.SendGetRequestRefreshToken(playerData.refreshToken);
        }
        else
        {
            //NON --> mettre ses credentials
            Debug.Log("Fichier introuvable à : " + Application.persistentDataPath);
            GameObject.Find("UsernameConnexionInput").GetComponent<TMP_InputField>().text = "";
            GameObject.Find("PasswordConnexionInput").GetComponent<TMP_InputField>().text = "";
            LeanTween.moveX(myMenu.voletConnexion.gameObject.GetComponent<RectTransform>(), -322f, 0.3f);
        }

        
    }

    public void Close()
    {
        myMenu.disableOnRequest.DisableAllInput(false);
        LeanTween.moveX(myMenu.voletConnexion.gameObject.GetComponent<RectTransform>(), 322f, 0.3f).setOnComplete(NotHere);
    }

    private void NotHere()
    {
        myMenu.disableOnRequest.EnableAllInput(false);
    }

}
