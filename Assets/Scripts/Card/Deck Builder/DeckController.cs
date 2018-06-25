using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//puts cards in deck and sends the data to the deckBuilder
public class DeckController : MonoBehaviour {

    [Tooltip("The Deck Container Layout")]
    public GameObject deckContainer = null;
    [Tooltip("The text to update to show the cards count")]
    public Text deckCountText = null;
    [Tooltip("Start and end color for the count deck cards number")]
    public Color[] flashingColors = new Color[2];

    [HideInInspector]
    public int numberOfCards = 0;
    [HideInInspector]
    public List<int> deckCards = new List<int>();
    [HideInInspector]
    public List<GameObject> deck = new List<GameObject>();
    [HideInInspector]
    public List<int> currentDeckCards = new List<int>();
    


    void Start () {
        deckContainer.GetComponentInParent<CardDropZone>().CardDroppedInDeckWithError +=new CardDroppedInDeckWithErrorEventHandler(NotifyErrorAllowedNumber);
        //deckContainer.GetComponentInParent<CardDropZone>().CardDroppedInDeck += new CardDroppedInDeckEventHandler(Filler);
    }

	void Update () {
        numberOfCards = deckContainer.transform.childCount;
        deckCountText.text = numberOfCards.ToString();
    }

    public void CreateDeck()
    {
        foreach (int cardId in deckCards)
        {
            GameObject draggableCardCopy = Instantiate(DeckBuilder.Instance.draggableCardPrefab);
            CardModel cardModel = draggableCardCopy.GetComponent<CardModel>();
            cardModel.cardId = cardId;
            cardModel.ToggleFaceFull(true);
            deck.Add(draggableCardCopy);
            draggableCardCopy.transform.SetParent(deckContainer.transform);
            draggableCardCopy.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void NotifyErrorAllowedNumber()
    {
        StartCoroutine(FlashOnError());
    }

    public IEnumerator FlashOnError()
    {
        deckCountText.color = flashingColors[0];
        yield return new WaitForSeconds(.2f);
        deckCountText.color = flashingColors[1];
    }

    public void OnClickSave()
    {
        if (numberOfCards != 5 )
        {
            StartCoroutine(FlashOnError());
            return;
        }
        else
        {
            DeckBuilder.Instance.notificationController.GetComponent<Animator>().SetTrigger(GetComponentInParent<DeckBuilder>().parameterDoneSavingHash);
            SaveDeckListToDB();
            transform.parent.GetComponentInChildren<PackController>().SavePackListToDB();
        }
    }

    public void SaveDeckListToDB()
    {
        UpdateDeckList();
        DataController.Instance.GetCardService().SaveCardsData(deckCards, CardService.SaveType.Deck);
    }

    public void UpdateDeckList()
    {
        deckCards = GetCurrentDeckCards();
    }

    public List<int> GetCurrentDeckCards()
    {
        currentDeckCards = new List<int>();
        foreach (Transform drawableCard in deckContainer.transform)
        {
            currentDeckCards.Add(drawableCard.gameObject.GetComponent<CardModel>().cardId);
        }
        return currentDeckCards;
    }

    public void Filler()
    {
        
    }
}
