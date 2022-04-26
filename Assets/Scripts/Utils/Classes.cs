using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{
    //JSON CLASS
    public class UserAccount
    {
        public string username;
        public string password;
        public string trainerName;
    }

    public class JwtRequest
    {
        public string username;
        public string password;
    }

    public class JwtResponse
    {
        public string token_type;
        public string access_token;
        public string expiresIn;
        public string refresh_token;
    }

    public class JwtRequestDTO
    {
        public string username;
    }

    public class NewRoom
    {
        public string name;
        public string password;
    }

    public class PokemonPageWithFilter
    {
        public int page;
        public int size;
        public string filter;
    }
    public class PokemonPage
    {
        public int page;
        public int size;
    }


    //CARD POKEMON
    public class OffensiveAttack
    {
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public List<string> targets { get; set; }
        public int power { get; set; }
    }

    public class DefensiveAttack
    {
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
    }

    public class Evolution
    {
        public string description { get; set; }
        public int pokemonId { get; set; }
        public string pokemonName { get; set; }
        public string evolutionType { get; set; }
    }

    public class PokemonCard
    {
        public int pokemonId { get; set; }
        public string name { get; set; }
        public string generation { get; set; }
        public int level { get; set; }
        public int lifePoint { get; set; }
        public int baseSpeed { get; set; }
        public float size { get; set; }
        public float weight { get; set; }
        public string type { get; set; }
        public string type2 { get; set; }
        public OffensiveAttack offensiveAttack { get; set; }
        public DefensiveAttack defensiveAttack { get; set; }
        public List<string> weaknesses { get; set; }
        public List<string> resistances { get; set; }
        public List<Evolution> evolutions { get; set; }
    }

    public class PokemonCardList
    {
        public List<PokemonCard> item { get; set; }
        public int currentPage { get; set; }
        public int size { get; set; }
        public int lastPage { get; set; }
        public int total { get; set; }
    }
}
