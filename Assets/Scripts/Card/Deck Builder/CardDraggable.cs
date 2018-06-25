using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDraggable : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler {

    [Tooltip("Tag of all the other cards (my cards) zone scrollRect")]
    public string myCardsScrollRectTag = "MyCards";
    [Tooltip("Tag of the Deck zone scrollRect")]
    public string deckScrollRectTag = "MyDeck";
    [Tooltip("Name of the gameObject that holds temporarly the cards")]
    public string tempCardsContainerName = "TempCardContainer";
    [HideInInspector]
    public Transform parentToReturnTo = null;
    [HideInInspector]
    public Transform placeHolderParent = null;

    private GameObject placeHolder = null;
    int newSiblingIndex = 0;

    //public Text testTextDrag = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //testTextDrag.text = "OnBeginDrag";
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(transform.parent);
        LayoutElement placeHolderLayoutElement = placeHolder.AddComponent<LayoutElement>();
        placeHolderLayoutElement.minWidth = GetComponent<LayoutElement>().minWidth;
        placeHolderLayoutElement.minHeight = GetComponent<LayoutElement>().minHeight;
        placeHolderLayoutElement.flexibleHeight = 0;
        placeHolderLayoutElement.flexibleWidth = 0;
        placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        parentToReturnTo = transform.parent;
        placeHolderParent = parentToReturnTo;
        transform.SetParent(GameObject.Find(tempCardsContainerName).transform);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //testTextDrag.text = newSiblingIndex.ToString();
        transform.position = eventData.position;
        if (placeHolder.transform.parent != placeHolderParent)
            placeHolder.transform.SetParent(placeHolderParent);

        
        for (int i = 0; i< placeHolderParent.childCount; i++)
        {
            if (placeHolderParent.transform.parent.tag == deckScrollRectTag)
            {
                if (transform.position.y > placeHolderParent.GetChild(i).position.y)
                {
                    newSiblingIndex = i;
                    if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;
                    break;
                }
            }else if (placeHolderParent.transform.parent.tag == myCardsScrollRectTag)
            {
                if (transform.position.x < placeHolderParent.GetChild(i).position.x && transform.position.y > placeHolderParent.GetChild(i).position.y)
                {
                    //making sure that it's getting close to the center of the card before making the change
                    if (transform.position.y <= placeHolderParent.GetChild(i).position.y + (placeHolderParent.GetChild(i).GetComponent<RectTransform>().rect.height / 4))
                    {
                        newSiblingIndex = i;
                        if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                            newSiblingIndex--;
                        break;
                    }
                }
                else
                {
                    newSiblingIndex++;
                }
            }
            else
            {
                break;
            }
        }
        placeHolder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //testTextDrag.text = placeHolder.transform.GetSiblingIndex() + " -> OnEndDrag" + "parent = " + parentToReturnTo.tag ;
        transform.SetParent(parentToReturnTo);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
        GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        Destroy(placeHolder);
    }

}
