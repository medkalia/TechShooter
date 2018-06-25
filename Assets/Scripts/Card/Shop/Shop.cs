using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

    [Tooltip("The prefab of the card to show in the showcase area")]
    public GameObject cardShowCasePrefab;
    [Tooltip("The prefab of the cards to show in the items area")]
    public GameObject cardShopItemPrefab;
    [Tooltip("The Animation controller that will take care of notificating the player when events happen")]
    public GameObject notificationController;
    [Tooltip("The confirmation text for buying")]
    public Text confirmationText;


    [HideInInspector]
    public List<int> shopCards = new List<int>();
    [HideInInspector]
    public ItemsController itemsController = null;
    [HideInInspector]
    public InspectorController inspectorController = null;

    [HideInInspector]
    public static Shop Instance { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

    }

    void Start()
    {

        inspectorController = GetComponentInChildren<InspectorController>();
        itemsController = GetComponent<ItemsController>();

        //deckCards = DataController.Instance.GetCardDataBase().DeckCards;
        //deckController.deckCards = deckCards;
        //deckController.CreateDeck();

        shopCards = DataController.Instance.GetCardService().ShopCards;
        itemsController.shopCards = shopCards;
        itemsController.CreateShopItems();
    }

    public void OnclickItem()
    {
        GameObject itemCardToInspect =  EventSystem.current.currentSelectedGameObject.gameObject;
        itemsController.itemCardToInspect = itemCardToInspect;
        inspectorController.InspectCard();
    }

    public void AskConfirmation()
    {
        notificationController.GetComponent<Animator>().SetTrigger(parameterOpenConfirmationHash);
        confirmationText.text = "Do you confirm Buying this tech card for <color=#F2F600FF>"
            + inspectorController.CardShowCase.GetComponent<CardModel>().cardPrice + " GC</color> ? \n <color>"
            + inspectorController.CardShowCase.GetComponent<CardModel>().cardName+ "</color> ";
    }

    public void OnClickIgnorePurchase()
    {
        notificationController.GetComponent<Animator>().SetTrigger(parameterCloseConfirmationHash);
    }

    public void OnClickConfirmPurchase()
    {
        DataController.Instance.GetCardService().SavePurchace(inspectorController.CardShowCase.GetComponent<CardModel>().cardId);
        itemsController.ClearItem(inspectorController.CardShowCase.GetComponent<CardModel>().cardId);
        inspectorController.ClearInspector();
        notificationController.GetComponent<Animator>().SetTrigger(parameterDoneBuyingHash);
    }

    public void OnClickQuitShop(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    [HideInInspector]
    public int parameterOpenConfirmationHash = Animator.StringToHash("openConfirmation");
    [HideInInspector]
    public int parameterDoneBuyingHash = Animator.StringToHash("doneBuying");
    [HideInInspector]
    public int parameterCloseConfirmationHash = Animator.StringToHash("closeConfirmation");


}
