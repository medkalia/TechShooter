using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InspectorController : MonoBehaviour {

    [Tooltip("The card showcase Container")]
    public GameObject showcaseContainer = null;
    [Tooltip("The buy button")]
    public Button buyButton = null;

    [HideInInspector]
    public GameObject CardShowCase = null;
    [HideInInspector]
    public ItemsController itemsController = null;

    private bool isShowcasing = false;

    private void Start()
    {
        buyButton.interactable = false;
        itemsController = GetComponent<ItemsController>();
        CardShowCase = Instantiate(Shop.Instance.cardShowCasePrefab);
        CardShowCase.GetComponent<CardModel>().ToggleFaceFull(false);
        CardShowCase.transform.SetParent(showcaseContainer.transform);
        RectTransform CardShowCaseRect = CardShowCase.GetComponent<RectTransform>();

        //CardShowCase.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
        CardShowCase.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        CardShowCaseRect.offsetMin = new Vector2(-2.5f, -1.87f);
        CardShowCaseRect.offsetMax = new Vector2(1.27f, 5.60f);
    }

    public void InspectCard()
    {
        Sprite oldShowCaseImage = null;
        if (!isShowcasing) oldShowCaseImage = CardShowCase.GetComponent<CardModel>().back;
        else oldShowCaseImage = CardShowCase.GetComponent<CardModel>().image.sprite;

        CardModel cardModel = CardShowCase.GetComponent<CardModel>();
        cardModel.cardId = itemsController.itemCardToInspect.GetComponent<CardModel>().cardId;
        cardModel.cardName = itemsController.itemCardToInspect.GetComponent<CardModel>().cardName.ToString();
        cardModel.cardPrice = itemsController.itemCardToInspect.GetComponent<CardModel>().cardPrice;
        cardModel.ToggleFaceFull(true);
        CardShowCase.GetComponent<CardFlipper>().FlipCard(oldShowCaseImage, cardModel.image.sprite);
        buyButton.interactable = true;
        isShowcasing = true;
    }

    public void OnClickBuy()
    {
        if (CardShowCase.GetComponent<CardModel>().cardPrice > DataController.Instance.GetProgressService().playerProgress.Gold)
        {
            itemsController.NotifyLackGoldNumber();
        }
        else
        {
            Shop.Instance.AskConfirmation();
        }
    }

    public void ClearInspector()
    {
        CardModel cardModel = CardShowCase.GetComponent<CardModel>();
        CardShowCase.GetComponent<CardFlipper>().FlipCard(cardModel.image.sprite, cardModel.back);
        isShowcasing = false;
        buyButton.interactable = false;
    }
}
