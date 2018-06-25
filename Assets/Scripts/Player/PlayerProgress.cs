using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[System.Serializable]
public class PlayerProgress{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int Gold { get; set; }
    public int LastLevel { get; set; }
    public bool HasFinishedGame { get; set; }

    public PlayerProgress(){ }

    public PlayerProgress(int id,int gold, int lastLevel, bool hasFinishedGame)
    {
        this.Id = Id;
        this.Gold = gold;
        this.LastLevel = lastLevel;
        this.HasFinishedGame = hasFinishedGame;
    }
}
