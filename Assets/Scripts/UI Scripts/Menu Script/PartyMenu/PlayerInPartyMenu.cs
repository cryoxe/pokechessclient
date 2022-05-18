using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInPartyMenu : MonoBehaviour
{

    [System.Serializable]
    public struct PlayerComponent
    {
        public TextMeshProUGUI nameOfplayer;
        public Image icon;

    }
    public PlayerComponent playerComponent;

    public void MakeMyPlayer(string nameOfplayer, int idPlayer)
    {
        playerComponent.nameOfplayer.text = nameOfplayer;
        playerComponent.icon.sprite = Resources.Load<Sprite>("Sprites/Party Assets/Avatars Party/" + idPlayer);
    }
}
