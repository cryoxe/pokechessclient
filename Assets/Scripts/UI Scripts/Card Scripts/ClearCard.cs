using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCard : MonoBehaviour
{
    public void destroyAllCard(){
        CreatePokemonCard[] allPokemonCard = GameObject.FindObjectsOfType<CreatePokemonCard>();
        foreach(CreatePokemonCard pokemonCard in allPokemonCard){
            Destroy(pokemonCard.gameObject);
        }    
    }


}
