using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void CardDroppedInDeckWithErrorEventHandler();
//public delegate void CardDroppedInDeckEventHandler();
public class CardDropZone : MonoBehaviour , IDropHandler, IPointerEnterHandler, IPointerExitHandler  {

    [Tooltip("Tag of the pack cards (my cards) zone scrollRect")]
    public string packScrollRectTag = "Pack";
    [Tooltip("Tag of the Deck zone scrollRect")]
    public string deckScrollRectTag = "Deck";
    [Tooltip("Name of the game object containing the cards")]
    public string ContainerName = "Container";
    [Tooltip("Number of allowed Deck cards")]
    public int deckCardsNumber = 5;
    [HideInInspector]
    public bool canDrop = false;

    public event CardDroppedInDeckWithErrorEventHandler CardDroppedInDeckWithError;
    //public event CardDroppedInDeckEventHandler CardDroppedInDeck;
    //public Text testText = null;


    private void Update()
    {
        //if (transform.Find(ContainerName).transform.childCount < deckCardsNumber)
        //    canDrop = true;
        //else
        //    canDrop = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //testText.text = "OnPointerEnter/ with : " + transform.Find(ContainerName).transform.childCount + "can drop = " + canDrop;
        if (eventData.pointerDrag == null)
            return;
        CardDraggable draggedCard = eventData.pointerDrag.GetComponent<CardDraggable>();
        if (draggedCard != null)
        {
            if (tag != draggedCard.parentToReturnTo.transform.parent.tag)
            {
                //Debug.Log("Different Zone");
                if (tag == deckScrollRectTag)
                {
                    if (transform.Find(ContainerName).transform.childCount < deckCardsNumber)
                    {
                        draggedCard.placeHolderParent = transform.Find(ContainerName).transform;
                    }
                    else
                    {
                        //Debug.Log("limit to "+ deckCardsNumber + " cards only in deck");
                        CardDroppedInDeckWithError();
                    }
                }
                else if (tag == packScrollRectTag)
                {
                    draggedCard.placeHolderParent = transform.Find(ContainerName).transform;
                }
            }
            else
            {
                //Debug.Log("same zone");
            }
                
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //testText.text = "OnPointerExit/ with : " + transform.Find(ContainerName).transform.childCount + "can drop = " + canDrop; 
        if (eventData.pointerDrag == null)
            return;
        CardDraggable draggedCard = eventData.pointerDrag.GetComponent<CardDraggable>();
        if (draggedCard != null && /*draggedCard.placeHolderParent == transform.Find(ContainerName).transform*/tag == draggedCard.parentToReturnTo.transform.parent.tag)
        {
            draggedCard.placeHolderParent = draggedCard.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //testText.text = "OnDrop/ with : " + transform.Find(ContainerName).transform.childCount + "can drop = " + canDrop; 
        CardDraggable draggedCard = eventData.pointerDrag.GetComponent<CardDraggable>();
        if (draggedCard != null)
        {
            if (tag == deckScrollRectTag)
            {
                if (transform.Find(ContainerName).transform.childCount <= deckCardsNumber && tag == draggedCard.placeHolderParent.transform.parent.tag)
                {
                    draggedCard.parentToReturnTo = transform.Find(ContainerName).transform;
                }
                else
                {
                    //Debug.Log("limit to "+ deckCardsNumber + " cards only in deck");
                    //CardDroppedInDeckWithError();
                }
                
            }
            else if (tag == packScrollRectTag)
            {
                draggedCard.parentToReturnTo = transform.Find(ContainerName).transform;
            }
            //CardDroppedInDeck();
        }
    }

    
}
