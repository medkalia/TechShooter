using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {

    List<int> cards;
    List<Card> tcards;
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }
    //creates a deck of shuffeled cards
    public void CreateDeck()
    {
        //if (cards == null) cards = new List<int>(DataController.Instance.GetCardDataBase().Cards.Keys);
        //TODO:FIX THIS
        //this has to be done to avoid getting a reference of the list of cards and so changing it 
        //foreach(int currentCard in DataController.Instance.GetCardService().DeckCards)
        //{
        //    Debug.Log("adding card with id :" + currentCard);
        //    cards.Add(currentCard);
        //}
        cards = DataController.Instance.GetCardService().DeckCards;
        IListExtensions.Shuffle(cards);
    }

    public int Pop ()
    {
        int temp = cards[0];
        cards.RemoveAt(0);
        return temp;
    }

    public int PopCard(int cardToRemove)
    {
        cards.RemoveAt(cards.IndexOf(cardToRemove));
        return cardToRemove;
    }

    public void Push (int card)
    {
        cards.Add(card);
    }

    
}
