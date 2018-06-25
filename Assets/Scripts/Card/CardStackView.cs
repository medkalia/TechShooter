using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour {

    public static CardStackView Instance { get; set; }

    [Tooltip("The prefab to instantiate")]
    public GameObject cardPrefab;
    [Tooltip("The cards container")]
    public GameObject deck;
    [Tooltip("The cards timer Bar")]
    public CardTimersBar cardTimerBar;


    private CardStack cardStack;
    private RectTransform start;
    private CardFlipper cardFlipper;

    void Start () {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        start = GetComponent<RectTransform>();
        cardStack = GetComponent<CardStack>();
        cardStack.CreateDeck();
        ShowCards();
        

    }


    void ShowCards()
    {
        int cardCount = 0;
        for (int i = 0; i <= 2; i++)
        {
            int currentCardId = cardStack.Pop();
            //cardStack.Push(currentCardId);

            GameObject cardCopy = Instantiate(cardPrefab);
            cardCopy.transform.SetParent(deck.transform);
            cardCopy.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            //RectTransform cardCopyRectTransform = cardCopy.GetComponent<RectTransform>();
            //cardCopyRectTransform.anchoredPosition = new Vector2(0.5f, 0.5f);
            //float co = cardOffset + (cardCount * cardCopyRectTransform.rect.width);
            //cardCopyRectTransform.localPosition = new Vector3(co, 0f, 0f);

            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            cardModel.cardId = currentCardId;
            cardModel.ToggleFaceMini(true);
            Button cardButton = cardModel.GetComponent<Button>();
            CardsLogic.Instance.AttachCardEffect(cardButton, DataController.Instance.GetCardService().GetCardById(currentCardId));

            cardCount++;
        }
    }

    public void Replace(GameObject card)
    {
        int oldCardId = card.GetComponent<CardModel>().cardId;
        Destroy(card);
        StartEffectTimer(oldCardId);
        int currentCardId = cardStack.Pop();
        cardStack.Push(oldCardId);

        GameObject cardCopy = Instantiate(cardPrefab);
        cardCopy.transform.SetParent(deck.transform);
        cardCopy.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        //RectTransform cardCopyRectTransform = cardCopy.GetComponent<RectTransform>();
        //cardCopyRectTransform.anchoredPosition = new Vector2(0.5f, 0.5f);
        //cardCopyRectTransform.localPosition = new Vector3(card.transform.localPosition.x, 0f, 0f);

        CardModel cardModel = cardCopy.GetComponent<CardModel>();
        cardModel.cardId = currentCardId;
        cardModel.ToggleFaceMini(true);
        Button cardButton = cardModel.GetComponent<Button>();
        CardsLogic.Instance.AttachCardEffect(cardButton, DataController.Instance.GetCardService().GetCardById(currentCardId));
    }

    public void StartEffectTimer (int cardId)
    {
        if (cardTimerBar != null )
        {
            cardTimerBar.AddTimer(cardId);
        }
    }

}
