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
        if(File.Exists(Application.persistentDataPath + "/RefreshToken.json"))
        {
            Debug.Log("Fichier trouvé à : " + Application.persistentDataPath);
            string path = Application.persistentDataPath + "/RefreshToken.json";
            StreamReader reader = new StreamReader(path); 
            RefreshToken playerData = JsonUtility.FromJson<RefreshToken>(reader.ReadToEnd());
            reader.Close();
            StaticVariable.refreshToken = "Bearer " + playerData.refreshToken;
            StaticVariable.theUsername = playerData.username;
            myMenu.connectedPlayerName.text = playerData.username;
            myMenu.requestGET.SendGetRequestRefreshToken(StaticVariable.refreshToken);
        }
        else
        {
            Debug.Log("Fichier introuvable à : " + Application.persistentDataPath);
            GameObject.Find("UsernameConnexionInput").GetComponent<TMP_InputField>().text = "";
            GameObject.Find("PasswordConnexionInput").GetComponent<TMP_InputField>().text = "";
            //montrer le volet
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
