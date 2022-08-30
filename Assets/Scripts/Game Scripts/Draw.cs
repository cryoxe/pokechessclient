using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour 
{
    private GameObject pokemonCardPrefab;
    public GameObject Content;

    public void Start()
    {
        pokemonCardPrefab = Resources.Load<GameObject>("Prefabs/Game/PokemonCard");
    }

    public void DrawACard()
    {
        if(Content.transform.childCount >= 10)
        {
            Debug.Log("Main pleine !!!");
        }
        else
        {
            GameObject thisCard= Instantiate(pokemonCardPrefab, Content.GetComponent<RectTransform>(), false);
            RectTransform cardRect = thisCard.GetComponent<RectTransform>();
            cardRect.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }


    }
}
