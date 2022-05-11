using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class Initialisation : MonoBehaviour
{
    //Utility
    [HideInInspector]public GameObject sceneManager;
    [HideInInspector]public PostProcessVolume blur;
    [HideInInspector]public MenuSwap menuSwap;
    [HideInInspector]public RawImage blank;
    [HideInInspector]public Animator pokeball;
    [HideInInspector]public PopUp popUp;
    [HideInInspector]public ClearCard clearCard;
    [HideInInspector]public DisableOnRequest disableOnRequest;

    //REQUEST
    [HideInInspector]public RequestGET requestGET;
    [HideInInspector]public RequestDELETE requestDELETE;
    [HideInInspector]public RequestPOST requestPOST;

    //Transistion
    [HideInInspector]public RectTransform transitionUp, transitionDown;

    //Menu de Connexion
    [HideInInspector]public GameObject accountPlaceholder;
    [HideInInspector]public Button connexionButton, subscribeButton, createConnexion, closeConnexion, createAccount, closeAccount;
    [HideInInspector]public Animator voletAccount, voletConnexion;
    [HideInInspector]public string usernameAccount, trainerNameAccount, passwordAccount;
    [HideInInspector]public string usernameConnexion, passwordConnexion;
    [HideInInspector]public Image dresseurImage;

    //Menu Principal
    [HideInInspector]public GameObject mainButtonPlaceholder;
    [HideInInspector]public Button playButton, optionButton, patchNoteButton;
    [HideInInspector]public Animator buttonSlide;

    //Menu Play Selection (jouer ou créer)
    [HideInInspector]public GameObject playButtonPlaceholder;
    [HideInInspector]public Button joinRoomButton, createRoomButton, backButton;

    //Menu Lobby
    [HideInInspector]public GameObject LobbyPlaceholder;
    [HideInInspector]public VerticalLayoutGroup LobbyFitter;

    //Menu Card Library
    [HideInInspector]public GameObject CardLibrary, Content;
    [HideInInspector]public CardPage cardPage;
    [HideInInspector]public Transform noBlur;
    [HideInInspector]public ChangePage changePage;
    [HideInInspector]public TextMeshProUGUI pageNumber;
    [HideInInspector]public ResetFilter resetFilter;



    // Start is called before the first frame update
    void Start()
    {
        //Utility
        sceneManager = GameObject.Find("SceneManager");
        blur = GameObject.Find("PostProcessingGO").GetComponent<PostProcessVolume>();
        menuSwap = GetComponent<MenuSwap>();
        blank = GameObject.Find("Blank").GetComponent<RawImage>();
        pokeball = GameObject.Find("PokéBall").GetComponent<Animator>();
        popUp = GetComponent<PopUp>();
        disableOnRequest = GameObject.Find("Canvases").GetComponent<DisableOnRequest>();


        //Transistion
        transitionUp = GameObject.Find("TransitionUp").GetComponent<RectTransform>();
        transitionDown = GameObject.Find("TransitionDown").GetComponent<RectTransform>();

        //Request
        requestGET = GetComponent<RequestGET>();
        requestPOST = GetComponent<RequestPOST>();
        requestDELETE = GetComponent<RequestDELETE>();

        //Menu de connexion
        accountPlaceholder = GameObject.Find("AccountPlaceholder");

        voletAccount = GameObject.Find("VoletAccount").GetComponent<Animator>();
        voletConnexion = GameObject.Find("VoletConnexion").GetComponent<Animator>();

        connexionButton = GameObject.Find("ButtonConnexion").GetComponent<Button>();
        subscribeButton = GameObject.Find("ButtonSubscribe").GetComponent<Button>();

        createConnexion = GameObject.Find("Connexion").GetComponent<Button>();
        closeConnexion = GameObject.Find("CloseConnexion").GetComponent<Button>();

        createAccount = GameObject.Find("CreateAccount").GetComponent<Button>();
        closeAccount = GameObject.Find("CloseAccount").GetComponent<Button>();

        dresseurImage = GameObject.Find("Dresseur").GetComponent<Image>();


        //Menu Principal 
        mainButtonPlaceholder = GameObject.Find("MainButtonPlaceholder");

        playButton = GameObject.Find("Play").GetComponent<Button>();
        optionButton = GameObject.Find("Options").GetComponent<Button>();
        patchNoteButton = GameObject.Find("Patch Note").GetComponent<Button>();

        buttonSlide = mainButtonPlaceholder.GetComponent<Animator>();


        //Menu Play Selection (jouer ou créer)
        playButtonPlaceholder = GameObject.Find("PlayButtonPlaceholder");

        joinRoomButton = GameObject.Find("PlayCreate").GetComponent<Button>();
        createRoomButton = GameObject.Find("PlayJoin").GetComponent<Button>();
        createRoomButton = GameObject.Find("Back").GetComponent<Button>();

        //Menu Lobby
        LobbyPlaceholder = GameObject.Find("LobbyMenu");
        LobbyFitter = GameObject.Find("fitter").GetComponent<VerticalLayoutGroup>();

        //Menu Card Library
        CardLibrary = GameObject.Find("CardLibrary");
        noBlur = GameObject.Find("PopUpCanvas").GetComponent<Transform>();
        Content = GameObject.Find("Content");
        cardPage = GetComponent<CardPage>();
        changePage = GetComponent<ChangePage>();
        clearCard = GetComponent<ClearCard>();
        pageNumber = GameObject.Find("PageNumber").GetComponent<TextMeshProUGUI>();
        resetFilter = GameObject.Find("ResetFilter").GetComponent<ResetFilter>();

        //initialisation
        DisableBlur();
        menuSwap.SwitchToConnexionMenu();
    }

    private void Update()
    {
        usernameAccount = GameObject.Find("UsernameInput").GetComponent<TMP_InputField>().text;
        trainerNameAccount = GameObject.Find("TrainerNameInput").GetComponent<TMP_InputField>().text;
        passwordAccount = GameObject.Find("PasswordInput").GetComponent<TMP_InputField>().text;

        usernameConnexion = GameObject.Find("UsernameConnexionInput").GetComponent<TMP_InputField>().text;
        passwordConnexion = GameObject.Find("PasswordConnexionInput").GetComponent<TMP_InputField>().text; 
    }

    public void EnableBlur()
    {
        blur.enabled = true;
    }
    public void DisableBlur()
    {
        blur.enabled = false;
    }
}
