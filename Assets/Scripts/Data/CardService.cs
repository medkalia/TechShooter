using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
//using UnityEditor;

public class CardService{




    [HideInInspector]
    public Dictionary<int, Card> Cards { get; set; }
    private List<int> deckCards;
    [HideInInspector]
    public List<int> PackCards { get; set; }
    [HideInInspector]
    public List<int> DeckCards
    {
        get
        {
            //Debug.Log("Deck Cards accessed size is now " + deckCards.Count);
            return deckCards;
        }
        set
        {
            deckCards = value;
            //Debug.Log("Deck Cards changed size is now " + deckCards.Count);
        }
    }
    [HideInInspector]
    public List<int> ShopCards { get; set; }

    public List<Card> CardsList { get; set; }

    public enum SaveType { Deck, Pack};

    public void BuildData()
    {

        CardsList = DataBase.Instance.connection.Table<Card>().ToList();
        Cards = CardsList.ToDictionary(x => x.Id, x => x);

        PackCards = GetPackCards().ToList().ConvertAll<int>(delegate (Card c)
        {
            return c.Id;
        });

        DeckCards = GetDeckCards().ToList().ConvertAll<int>(delegate (Card c)
        {
            return c.Id;
        });

        ShopCards = new List<int>(Cards.Keys);
        ShopCards = ShopCards.Except(PackCards).ToList();
        ShopCards = ShopCards.Except(DeckCards).ToList();

    }

    public IEnumerable<Card> GetPackCards()
    {
        return DataBase.Instance.connection.Table<Card>().Where(x => x.IsInPack == 1);
    }

    public IEnumerable<Card> GetDeckCards()
    {
        return DataBase.Instance.connection.Table<Card>().Where(x => x.IsInDeck == 1);
    }

    public Card GetCardById(int id)
    {
        if (!Cards.ContainsKey(id))
        {
            Debug.LogWarning("No card with id " + id + " found");
            return null;
        }
        return Cards[id];
    }

    public void SavePurchace (int boughtCardId)
    {
        
        Card cardToUpdate = Cards[boughtCardId];
        cardToUpdate.IsInDeck = 0;
        cardToUpdate.IsInPack = 1;
        DataBase.Instance.connection.RunInTransaction(() =>
        {
            DataBase.Instance.connection.Update(cardToUpdate);
        });
        PackCards.Add(boughtCardId);
        ShopCards.Remove(boughtCardId);
    }

    public void SaveCardsData(List<int> cardsList, SaveType saveType )
    {
        if (saveType == SaveType.Deck) DeckCards = cardsList;
        else PackCards = cardsList;

        foreach (int id in cardsList)
        {
            if (saveType == SaveType.Deck)
            {
                Card cardToUpdate = Cards[id];
                cardToUpdate.IsInDeck = 1;
                cardToUpdate.IsInPack = 0;
                DataBase.Instance.connection.RunInTransaction(() =>
                {
                    DataBase.Instance.connection.Update(cardToUpdate);
                });
            }else
            {
                Card cardToUpdate = Cards[id];
                cardToUpdate.IsInDeck = 0;
                cardToUpdate.IsInPack = 1;
                DataBase.Instance.connection.RunInTransaction(() =>
                {
                    DataBase.Instance.connection.Update(cardToUpdate);
                });
            }
        }
    }


    //private void SaveShopCardsData(List<CollectedCard> deckCardsList)
    //{
    //    string dataAsJson = JsonConvert.SerializeObject(deckCardsList);
    //    string path = deckCardsDataFilePath;
    //    StreamWriter writer = new StreamWriter(path, false);
    //    writer.Write(dataAsJson);
    //    writer.Close();
    //    AssetDatabase.ImportAsset(path);
    //}

}
