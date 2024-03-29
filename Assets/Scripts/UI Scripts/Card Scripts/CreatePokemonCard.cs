﻿using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct PokemonCardListText
{
    public TextMeshProUGUI level;
    public TextMeshProUGUI name;
    public TextMeshProUGUI ID;
    public TextMeshProUGUI weightHeight;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI health;
    public TextMeshProUGUI attackName;
    public TextMeshProUGUI attackEffect;
    public TextMeshProUGUI attackDamage;
    public TextMeshProUGUI defenseName;
    public TextMeshProUGUI defenseEffect;
}
[System.Serializable]
public struct PokemonCardListImage
{
    public Image backGroundTypeMono;
    public Image backGroundTypeOne;
    public Image backGroundTypeTwo;
    public Image picture;
    public Image pictureBackgroundMono;
    public Image pictureBackgroundLeft;
    public Image pictureBackgroundRight;
    public Image attackButton;
    public Image defenseButton;
}
[System.Serializable]
public struct PokemonCardListTransform
{
    public RectTransform cursor;
    public RectTransform evolutionHolder;
}
[System.Serializable]
public struct PokemonCardListSlider
{
    public Slider barOfHealth;
    public Slider wLeft;
    public Slider sLeft;
}

public class CreatePokemonCard : MonoBehaviour
{
    //private string myJsonPokemon, path;
    private Initialisation myMenu;
    private int absoluteSpeed;
    public PokemonCardListText pokemonCardListText;
    public PokemonCardListImage pokemonCardListImage;
    public PokemonCardListSlider pokemonCardListSlider;
    public PokemonCardListTransform pokemonCardListTransform;
    
    //void Start(){
        //myMenu = GameObject.Find("SceneManager").GetComponent<Initialisation>();
        //myMenu.clearCard.cardToDestroy.Add(gameObject);
    //}
    public void MakeMyCard(JSONNode pokemonCard)
    {

        //Affichage simple :
        pokemonCardListText.name.text = pokemonCard["name"];
        gameObject.name = pokemonCard["name"];
        pokemonCardListText.level.text = pokemonCard["level"];
        //ID
        string pokeID = pokemonCard["pokemonId"];
        string absolutePokeID = pokeID.Substring(1, 3);
        pokemonCardListText.ID.text = "#"+ absolutePokeID;

        //taille et poids :
        pokemonCardListText.weightHeight.text = "Taille : " + pokemonCard["size"] + " m\nPoid : " + pokemonCard["weight"]+ " kg";

        //VITESSE
        string Speed = pokemonCard["baseSpeed"];
        pokemonCardListText.speed.text = Speed;
        absoluteSpeed = int.Parse(Speed);
        if (absoluteSpeed > 140){absoluteSpeed = 100;}
        else if (absoluteSpeed < 0){absoluteSpeed = 0;}
        int range1Max = 140;
        int range1Min = 0;
        int range2Max = -105;
        int range2Min = 100;
        float normalizedValue = Mathf.InverseLerp(range1Min, range1Max, absoluteSpeed);
        float result = Mathf.Lerp(range2Min, range2Max, normalizedValue);
        pokemonCardListTransform.cursor.eulerAngles = new Vector3(0, 0, result);

        //VIE
        pokemonCardListText.health.text = pokemonCard["lifePoint"];
        pokemonCardListSlider.barOfHealth.maxValue = pokemonCard["lifePoint"];
        pokemonCardListSlider.barOfHealth.value = pokemonCard["lifePoint"];

        //BackgroundType :
        Image monoType = pokemonCardListImage.backGroundTypeMono;
        Image typeOne = pokemonCardListImage.backGroundTypeOne;
        Image typeTwo = pokemonCardListImage.backGroundTypeTwo;

        //BackgroundPictureType:
        Image monoTypeBack = pokemonCardListImage.pictureBackgroundMono;
        Image typeLeftBack = pokemonCardListImage.pictureBackgroundLeft;
        Image typeRightBack = pokemonCardListImage.pictureBackgroundRight;

        //type :
        if (pokemonCard["type2"] == null)
        {
            Sprite TypeSprite = Resources.Load<Sprite>("Sprites/PokémonTemplate/Cartes/types/entier/" + pokemonCard["type"]);
            monoType.enabled = true;
            monoType.sprite = TypeSprite;
            typeOne.enabled = false;
            typeTwo.enabled = false;

            //BackgroundPicture
            Sprite BackgroundPicture = Resources.Load<Sprite>("Sprites/PokémonTemplate/Fonds/entier/" + pokemonCard["type"]);
            monoTypeBack.enabled = true;
            monoTypeBack.sprite = BackgroundPicture;
            typeLeftBack.enabled = false;
            typeRightBack.enabled = false;
        }
        else
        {
            Sprite TypeOneSprite = Resources.Load<Sprite>("Sprites/PokémonTemplate/Cartes/types/gauche/L" + pokemonCard["type"]);
            Sprite TypeTwoSprite = Resources.Load<Sprite>("Sprites/PokémonTemplate/Cartes/types/droite/R" + pokemonCard["type2"]);
            monoType.enabled = false;
            typeOne.enabled = true;
            typeOne.sprite = TypeOneSprite;
            typeTwo.enabled = true;
            typeTwo.sprite = TypeTwoSprite;

            //BackgroundPicture
            Sprite BackgroundPictureLeft = Resources.Load<Sprite>("Sprites/PokémonTemplate/Fonds/gauche/" + pokemonCard["type"]);
            Sprite BackgroundPictureRight = Resources.Load<Sprite>("Sprites/PokémonTemplate/Fonds/droite/D" + pokemonCard["type2"]);
            monoTypeBack.enabled = false;
            typeLeftBack.enabled = true;
            typeLeftBack.sprite = BackgroundPictureLeft;
            typeRightBack.enabled = true;
            typeRightBack.sprite = BackgroundPictureRight;
        }

        //Image :
        Sprite Image = Resources.Load<Sprite>("Sprites/PokémonTemplate/PokemonImage/" + pokemonCard["pokemonId"]);
        pokemonCardListImage.picture.sprite = Image;
        if (pokemonCardListImage.picture.sprite == null)
        {
            Image = Resources.Load<Sprite>("Sprites/PokémonTemplate/PokemonImage/MissingNumber");
            pokemonCardListImage.picture.sprite = Image;
        }

        //ATTACK
        pokemonCardListText.attackEffect.enableAutoSizing = true;
        JSONNode pokemonCardOffensive = pokemonCard["offensiveAttack"];
        pokemonCardListText.attackName.text = pokemonCardOffensive["name"];
        pokemonCardListText.attackEffect.text = pokemonCardOffensive["description"];

        pokemonCardListText.attackEffect.ForceMeshUpdate();
        float autoSize = pokemonCardListText.attackEffect.fontSize;
        pokemonCardListText.attackEffect.enableAutoSizing  = false;
        pokemonCardListText.attackEffect.fontSize = autoSize;

        if (pokemonCardListText.attackEffect.fontSize > 45){pokemonCardListText.attackEffect.fontSize = 45;}

        pokemonCardListText.attackDamage.text = pokemonCardOffensive["power"];
        Image = Resources.Load<Sprite>("Sprites/PokémonTemplate/attaques/boutons attaque/B" + pokemonCardOffensive["type"]);
        pokemonCardListImage.attackButton.sprite = Image;

        //DEFENSE
        pokemonCardListText.defenseEffect.enableAutoSizing  = true;
        JSONNode pokemonCardDefensive = pokemonCard["defensiveAttack"];
        pokemonCardListText.defenseName.text = pokemonCardDefensive["name"];
        pokemonCardListText.defenseEffect.text = pokemonCardDefensive["description"];

        pokemonCardListText.attackEffect.ForceMeshUpdate();
        autoSize = pokemonCardListText.defenseEffect.fontSize;
        pokemonCardListText.defenseEffect.enableAutoSizing  = false;
        pokemonCardListText.defenseEffect.fontSize = autoSize;

        if (pokemonCardListText.defenseEffect.fontSize > 45){pokemonCardListText.defenseEffect.fontSize = 45;}

        Image = Resources.Load<Sprite>("Sprites/PokémonTemplate/attaques/boutons attaque/B" + pokemonCardDefensive["type"]);
        pokemonCardListImage.defenseButton.sprite = Image;

        //FAIBLESSE
        JSONNode pokemonCardWeaknesses = pokemonCard["weaknesses"];
        Slider wBar = pokemonCardListSlider.wLeft;
        wBar.value = 50 * (pokemonCardWeaknesses.Count-1);
        string[] listOfType = pokemonCard["weaknesses"];
        int iteration = 0;
        foreach (string weakness in listOfType)
        {
            GameObject typePrefab = Resources.Load<GameObject>("Prefabs/TypePrefab/"+ pokemonCardWeaknesses[iteration]);
            float pos = wBar.transform.position.x + 1 + (2.5f*iteration);
            Instantiate(typePrefab, new Vector3(pos, wBar.transform.position.y, wBar.transform.position.z), Quaternion.identity, wBar.transform);
            iteration += 1;
        }

        //RESISTANCES
        JSONNode pokemonCardStrenght = pokemonCard["resistances"];
        Slider rBar = pokemonCardListSlider.sLeft;
        rBar.value = 50 * (pokemonCardStrenght.Count-1);
        listOfType = pokemonCard["resistances"];
        iteration = 0;
        foreach (string weakness in listOfType)
        {
            //print("Prefabs/TypePrefab/" + pokemonCardStrenght[iteration]);
            GameObject typePrefab = Resources.Load<GameObject>("Prefabs/TypePrefab/" + pokemonCardStrenght[iteration]);
            float pos = rBar.transform.position.x + 1 + (2.5f* iteration);
            Instantiate(typePrefab, new Vector3(pos, rBar.transform.position.y, rBar.transform.position.z), Quaternion.identity, rBar.transform);
            iteration += 1;
        }
        //EVOLUTION
        JSONNode pokemonCardEvolution = pokemonCard["evolutions"];
        RectTransform holder = pokemonCardListTransform.evolutionHolder;
        JSONNode myEvo = pokemonCardEvolution[0];
        for (int i = 0; i < pokemonCard["evolutions"].Count; i++)
        {
            GameObject evoPrefab = Resources.Load<GameObject>("Prefabs/EvoPrefab/" + myEvo["evolutionType"]);
            //print(myEvo["evolutionType"]);
            float pos = holder.position.y - (120 * i);
            GameObject EvoInstance = Instantiate(evoPrefab, new Vector3(holder.position.x, pos, holder.position.z), Quaternion.identity, holder) as GameObject;
            EvolutionUI EvoComponent = EvoInstance.GetComponent<EvolutionUI>();
            EvoComponent.evolutionName.text = myEvo["name"];
            Image = Resources.Load<Sprite>("Sprites/PokémonTemplate/PokemonImage/Cartouche/" + myEvo["pokemonId"]);
            EvoComponent.image.sprite = Image;
            EvoComponent.condition.text = myEvo["description"];
        }
    }
}