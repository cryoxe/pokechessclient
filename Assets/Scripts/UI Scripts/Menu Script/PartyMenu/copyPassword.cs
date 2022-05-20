using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class copyPassword : MonoBehaviour
{
    private TextMeshProUGUI successMessage;
    void Start(){
        successMessage = GameObject.Find("SuccessMessage").GetComponent<TextMeshProUGUI>();
        ResetString();
    }
    public void CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = StaticVariable.passwordOfTheParty;
        successMessage.text = "Succès !";
        Invoke("ResetString", 0.85f);
    }

    private void ResetString(){successMessage.text = "";}
}
