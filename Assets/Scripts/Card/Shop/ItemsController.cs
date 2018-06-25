using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemsController : MonoBehaviour {

    [Tooltip("The Shop items Container Layout")]
    public GameObject shopItemsContainer = null;
    [Tooltip("The count of the cards in the shop")]
    public Text shopCardsCountText = null;
    [Tooltip("The player's gold count ")]
    public Text goldCount = null;
    [Tooltip("Start and end color for the count gold text")]
    public Color[] flashingColors = new Color[2];

    [HideInInspector]
    public List<int> shopCards = new List<int>();
    [HideInInspector]
    public List<GameObject> shopItems = new List<GameObject>();
    [HideInInspector]
    public GameObject itemCardToInspect = null;


    void Start () {
        shopCardsCountText.text = DataController.Instance.GetCardService().ShopCards.Count + " Cards Available";
        goldCount.text = DataController.Instance.GetProgressService().playerProgress.Gold.ToString();
    }

    public void CreateShopItems()
    {
        foreach (int cardId in shopCards)
        {
            GameObject cardShopItemPrefabCopy = Instantiate(Shop.Instance.cardShopItemPrefab);
            CardModel cardModel = cardShopItemPrefabCopy.GetComponent<CardModel>();
            cardModel.cardId = cardId;
            cardModel.cardPrice = DataController.Instance.GetCardService().GetCardById(cardId).GoldCost;
            cardModel.cardName = DataController.Instance.GetCardService().GetCardById(cardId).CardName;
            cardModel.ToggleFaceFull(true);
            shopItems.Add(cardShopItemPrefabCopy);
            cardShopItemPrefabCopy.GetComponent<Button>().onClick.AddListener(Shop.Instance.OnclickItem);
            cardShopItemPrefabCopy.transform.SetParent(shopItemsContainer.transform);
            cardShopItemPrefabCopy.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void ClearItem(int boughtCardId)
    {
        
        foreach (Transform cardItem in shopItemsContainer.transform)
        {
            if (cardItem.gameObject.GetComponent<CardModel>().cardId == boughtCardId)
            {
                int newGoldAmount = DataController.Instance.GetProgressService().playerProgress.Gold - cardItem.gameObject.GetComponent<CardModel>().cardPrice;
                goldCount.text = newGoldAmount.ToString();
                shopCardsCountText.text = DataController.Instance.GetCardService().ShopCards.Count + " Cards Available";
                DataController.Instance.GetProgressService().UpdateGold(newGoldAmount);
                Destroy(cardItem.gameObject);
                break;
            }
        }

    }

    public void NotifyLackGoldNumber()
    {
        StartCoroutine(FlashOnError());
    }

    public IEnumerator FlashOnError()
    {
        goldCount.color = flashingColors[0];
        yield return new WaitForSeconds(.2f);
        goldCount.color = flashingColors[1];
    }
}
