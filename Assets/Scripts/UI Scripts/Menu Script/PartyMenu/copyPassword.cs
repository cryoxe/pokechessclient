using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class copyPassword : MonoBehaviour
{
    private TextMeshProUGUI successMessage;
    public void CopyToClipboard()
    {
        successMessage = GameObject.Find("SuccessMessage").GetComponent<TextMeshProUGUI>();
        GUIUtility.systemCopyBuffer = StaticVariable.passwordOfTheParty;
        successMessage.text = "Succès !";
        Invoke("ResetString", 0.85f);
    }

    private void ResetString(){successMessage.text = "";}
}
