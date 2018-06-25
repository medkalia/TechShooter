using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//puts cards in pack and sends the data to the deckBuilder
public class PackController : MonoBehaviour {

    [Tooltip("The Pack Container Layout")]
    public GameObject packContainer = null;
    [Tooltip("The count of the cards in the shop")]
    public Text shopCardsCountText = null;

    [HideInInspector]
    public List<int> packCards = new List<int>();
    [HideInInspector]
    public List<GameObject> pack = new List<GameObject>();

    void Start () {
        shopCardsCountText.text = "SHOP(" + DataController.Instance.GetCardService().ShopCards.Count+ ")";
    }

    public void CreatePack()
    {
        foreach (int cardId in packCards)
        {
            GameObject draggableCardCopy = Instantiate(DeckBuilder.Instance.draggableCardPrefab);
            CardModel cardModel = draggableCardCopy.GetComponent<CardModel>();
            cardModel.cardId = cardId;
            cardModel.ToggleFaceFull(true);
            pack.Add(draggableCardCopy);
            draggableCardCopy.transform.SetParent(packContainer.transform);
            draggableCardCopy.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Cards shop", LoadSceneMode.Single);
    }

    public void SavePackListToDB()
    {
        UpdatePackList();
        DataController.Instance.GetCardService().SaveCardsData(packCards, CardService.SaveType.Pack);
    }

    public void UpdatePackList()
    {
        packCards = new List<int>();
        foreach (Transform drawableCard in packContainer.transform)
        {
            packCards.Add(drawableCard.gameObject.GetComponent<CardModel>().cardId);
        }
    }
}
