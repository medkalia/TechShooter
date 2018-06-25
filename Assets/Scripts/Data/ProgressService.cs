using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ProgressService {

    public PlayerProgress playerProgress;

    public int levelGoldCount = 0;

    public void BuildDataBase()
    {
        playerProgress = DataBase.Instance.connection.Table<PlayerProgress>().First();
    }

    public void UpdateGold(int newGoldAmount)
    {
        playerProgress.Gold =  newGoldAmount;
        DataBase.Instance.connection.RunInTransaction(() =>
        {
            DataBase.Instance.connection.Update(playerProgress);
        });
    }

    public void UpdateLevel()
    {
        playerProgress.LastLevel++;
        DataBase.Instance.connection.RunInTransaction(() =>
        {
            DataBase.Instance.connection.Update(playerProgress);
        });
    }

    public void SaveFinish()
    {
        playerProgress.HasFinishedGame = true;
        DataBase.Instance.connection.RunInTransaction(() =>
        {
            DataBase.Instance.connection.Update(playerProgress);
        });
    }
}
