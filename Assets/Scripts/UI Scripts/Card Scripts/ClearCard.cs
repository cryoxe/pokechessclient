using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCard : MonoBehaviour
{
    public List<GameObject> cardToDestroy = new List<GameObject>();

    public void destroyAllCard(){
        foreach(GameObject g in cardToDestroy){
            Destroy(g);
        }
        cardToDestroy.Clear();        
    }
}
