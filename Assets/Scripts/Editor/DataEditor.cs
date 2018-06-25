using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;

public class DataEditor : EditorWindow
{
    private string databaseName = "Hope1DB";
    private List<Card> cardsList = null;
    public CardsData cardsData = new CardsData() ;


    [MenuItem ("Window/ Data Editor")]
    static void Init()
    {
        DataEditor window = (DataEditor) EditorWindow.GetWindow(typeof(DataEditor));
        window.Show();
    }

    private void OnGUI()
    {
        if (cardsList != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("cardsData");

            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            //if (GUILayout.Button("Save data"))
            //{
            //    SaveCardsData();
            //} 
        }

        //if (GUILayout.Button("Load data"))
        //{
        //    LoadCardsData();
        //}


        if (GUILayout.Button("Set Cards Default"))
        {
            DataBase.Instance.RestCardTable();
            CreateDefaultCardData();
        }

        if (GUILayout.Button("Set Progress Default"))
        {
            DataBase.Instance.RestProgressTable();
            CreateDefaultProgressData();
        }

        //if (GUILayout.Button("Set Notification Default"))
        //{
        //    DataBase.Instance.ResetNotificationTable();
        //    CreateDefaultNotificationData();
        //}

        if (GUILayout.Button("Reset Player Prefs"))
        {
            ResetPlayerPrefs();
        }
    }

    private void CreateDefaultCardData()
    {
        DataBase.Instance.connection.InsertAll(new[]{
            new Card(1,"card_power_jump","Power Jump",3,9.5f,15.0f,"Utility","1",0,1,0),
            new Card(2,"card_rafale","Rafale",5,0.1f,10f,"Offensive","1",0,1,0),
            new Card(3,"card_regenerate","Regenerate",6,7,10f,"Defensive","1",70,0,0),
            new Card(4,"card_heal","Heal",5,0.5f,0f,"Defensive","1",0,1,0),
            new Card(5,"card_overcharge","Overcharge",4,0.5f,10,"Offensive","1",50,0,0),
            new Card(6,"card_haste","Haste",3,8,15,"Utility","1",50,1,0),
            new Card(7,"card_power_skin","Power Skin",6,0.5f,10,"Utility","1",150,0,0),
            new Card(8,"card_tech_rush","Tech Points",3,6,0,"Utility","1",100,0,0),
            new Card(9,"card_tech_urge","Tech Urge",3,2,5,"Utility","1",0,1,0),
            new Card(10,"card_barrier","Barrier",6,0,5,"Defensive","1",150,0,0),
        });
    }

    private void CreateDefaultProgressData()
    {
        DataBase.Instance.connection.Insert(new PlayerProgress(1,0,0,false));
    }

    private void CreateDefaultNotificationData()
    {
        DataBase.Instance.connection.InsertAll(new[]{
            new Notification (0, "First game welcome",false,false,"Welcome to tech shooter"),
            new Notification (1, "Game finishNotif",false,false,"Well Done! you've finished all the levels. More levels are incoming"),
            new Notification (2, "Rate invite",false,false,"Please Consider rating Tech Shooter, it helps us a lot =)"),
        });
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void LoadCardsData()
    {
        cardsList = DataBase.Instance.connection.Table<Card>().ToList();
        if (cardsList != null)
            cardsData.cardsList = cardsList;
        else
            cardsData.cardsList = new List<Card>();
    }

    private void SaveCardsData()
    {
        //string dataAsJson = JsonConvert.SerializeObject(cardsList);
        //FileUtil.SaveFile(allCardsDataFilePath, dataAsJson);

        //string path = FileUtil.GetPath(allCardsDataFilePath);
        //StreamWriter writer = new StreamWriter(path, false);
        //writer.Write(dataAsJson);
        //writer.Close();
        //AssetDatabase.ImportAsset(path);
    }
}