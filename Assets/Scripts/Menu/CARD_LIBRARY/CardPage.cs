using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;

public class CardPage : MonoBehaviour
{
    private Initialisation myMenu;
    public  GameObject pokemonCardPrefab;

    public void Start()
    {
        myMenu = GetComponent<Initialisation>();
        pokemonCardPrefab = Resources.Load<GameObject>("Prefabs/PokemonCard");
    }

    public void CreateCardInstanceForPage(JSONNode listOfPokemon)
    {
        Debug.LogWarning("Begin INSTANTIATE");
        foreach (JSONNode pokemon in listOfPokemon)
        {
            GameObject thisCard= Instantiate(pokemonCardPrefab, myMenu.Content.transform, false);
            thisCard.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.1f);
            CreatePokemonCard script = thisCard.GetComponent<CreatePokemonCard>();
            script.MakeMyCard(pokemon);
        }
    }
}
