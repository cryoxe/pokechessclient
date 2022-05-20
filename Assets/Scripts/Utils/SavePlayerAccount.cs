using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEngine.Windows;
using UnityEngine;

public class SavePlayerAccount : MonoBehaviour
{
    public void SaveIntoJson(RefreshToken refreshToken){
        string refreshTokenJson = JsonUtility.ToJson(refreshToken);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/RefreshToken.json", refreshTokenJson);
        Debug.Log("Saved the new token");
        UnityEditor.AssetDatabase.Refresh();
    }
}
