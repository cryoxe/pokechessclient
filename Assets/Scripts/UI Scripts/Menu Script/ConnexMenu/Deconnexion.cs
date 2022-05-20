using System.Collections;
using System.IO;
using UnityEngine;

public class Deconnexion : MonoBehaviour
{
    private Initialisation myMenu;
    
    void Start()
    {
        myMenu = FindObjectOfType<Initialisation>();
    }
    public void Deconnecting()
    {
        myMenu.disableOnRequest.DisableAllInput(false);
        File.Delete(Application.persistentDataPath + "/RefreshToken.json");
        UnityEditor.AssetDatabase.Refresh();
        myMenu.menuSwap.Transition(0);
    }
}
