using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StaticVariable
{

    public static string accessToken;
    public static string refreshToken;

    public static string theUsername = "<MISSIGNO>";
    public static bool isOwner = false;

    public static int idForMenuSwitch;

    public static int pageNumber = 1;
    public static int lastPage;

    public static string apiUrl = "https://pokechess-card-game.herokuapp.com/api/v1/";

    public static JSONNode currentPage;
    public static JSONNode currentPokemonJSON;

    public static GameObject pokemonCard = Resources.Load<GameObject>("Prefabs/PokemonCard");

    public static bool isPopUpActive = false;

    public static bool isFilterEnable = false;
    public static string filter;

    public static string nameOfThePartyIn = null;
    public static string passwordOfTheParty = null;

    public static bool isConnexionFieldsOn = false;
    public static bool isAccountFieldsOn = false;

}
