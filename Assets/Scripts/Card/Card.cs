using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SQLite4Unity3d;


[System.Serializable]
public class Card {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Slug { get; set; }
    public string CardName { get; set; }
    public int TechPointCost { get; set; }
    public float EffectModifier { get; set; }
    public float EffectDuration { get; set; }
    public string CardType { get; set; }
    public string CardRarity { get; set; }
    public int GoldCost { get; set; }
    public string FullImageName { get; set; }
    public string MiniImageName { get; set; }
    public string MicroImageName { get; set; }
    public int IsInDeck { get; set; }
    public int IsInPack { get; set; }


    public Card()
    { }

    public Card(int id, string slug, string cardName, int techPointCost, float effectModifier, float effectDuration, string cardType, string cardRarity, int goldCost, int isInDeck, int isInPack)
    {
        this.Id = id;
        this.Slug = slug;
        this.CardName = cardName;
        this.TechPointCost = techPointCost;
        this.EffectModifier = effectModifier;
        this.EffectDuration = effectDuration;
        this.CardType = cardType;
        this.CardRarity = cardRarity;
        this.GoldCost = goldCost;
        this.IsInDeck = isInDeck;
        this.IsInPack = isInPack;
        this.FullImageName = "Cards/Full/" + slug;
        this.MiniImageName = "Cards/Mini/" + slug;
        this.MicroImageName = "Cards/Micro/" + slug;
        
    }
}
