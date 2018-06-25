using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardModel : MonoBehaviour {

    [Tooltip("Card Back image")]
    public GameObject priceTag = null;
    [Tooltip("Card Back image")]
    public Sprite back;

    [HideInInspector]
    public int cardId;
    [HideInInspector]
    public int cardPrice;
    [HideInInspector]
    public string cardName;
    [HideInInspector]
    public Image image ;
    

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ToggleFaceMini (bool showFace)
    {

        if (showFace)
        { 
            image.sprite = Resources.Load<Sprite>(DataController.Instance.GetCardService().GetCardById(cardId).MiniImageName);  
            if (priceTag != null)
            {
                priceTag.SetActive(true);
                priceTag.GetComponentInChildren<Text>(true).text = DataController.Instance.GetCardService().GetCardById(cardId).GoldCost.ToString();
            }
        }
        else
        {
            image.sprite = back ;
            if (priceTag != null)
            {
                priceTag.SetActive(false);
            }
        }
    }

    public void ToggleFaceFull(bool showFace)
    {

        if (showFace)
        {

            image.sprite = Resources.Load<Sprite>(DataController.Instance.GetCardService().GetCardById(cardId).FullImageName);
            if (priceTag != null)
            {
                priceTag.SetActive(true);
                priceTag.GetComponentInChildren<Text>(true).text = cardPrice.ToString();
            }
        }
        else
        {
            image.sprite = back;
            if (priceTag != null)
            {
                priceTag.SetActive(false);
            }
        }
    }

}
