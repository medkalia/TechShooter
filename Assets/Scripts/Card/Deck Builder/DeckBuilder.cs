using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
//brings and saves data from database
public class DeckBuilder : MonoBehaviour {
    [Tooltip("The prefab of the cards to show in the deck builder")]
    public GameObject draggableCardPrefab;
    [Tooltip("The Animation controller that will take care of notificating the player when events happen")]
    public GameObject notificationController;


    [HideInInspector]
    public List<int> deckCards = new List<int>();
    [HideInInspector]
    public List<int> packCards = new List<int>();
    [HideInInspector]
    public DeckController deckController = null;
    [HideInInspector]
    public PackController packController = null;

    [HideInInspector]
    public static DeckBuilder Instance { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

    }

    void Start () {

        deckController = GetComponentInChildren<DeckController>();
        packController = GetComponentInChildren<PackController>();

        deckCards = DataController.Instance.GetCardService().DeckCards;
        deckController.deckCards = deckCards;
        deckController.CreateDeck();

        packCards = DataController.Instance.GetCardService().PackCards;
        packController.packCards = packCards;
        packController.CreatePack();
    }
	
	void Update () {
		
	}

    public void OnClickQuitDeckBuilder(string sceneName)
    {
        if (deckController.numberOfCards != 5)
        {
            deckController.NotifyErrorAllowedNumber();
            
        }
        else
        {
            if (IListExtensions.AreEqual(deckController.deckCards, deckController.GetCurrentDeckCards()))
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            else
            {
                notificationController.GetComponent<Animator>().SetTrigger(parameterConfirmationHash);
            }
        } 
    }

    public void OnClickIgnoreChanges(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void OnClickIgnoreQuit()
    {
        notificationController.GetComponent<Animator>().SetTrigger(parameterCloseConfirmationHash);
    }

    [HideInInspector]
    public int parameterDoneSavingHash = Animator.StringToHash("doneSaving");
    [HideInInspector]
    public int parameterConfirmationHash = Animator.StringToHash("confirmation");
    [HideInInspector]
    public int parameterCloseConfirmationHash = Animator.StringToHash("closeConfirmation");
}

