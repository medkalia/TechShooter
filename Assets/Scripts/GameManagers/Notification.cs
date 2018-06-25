using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[System.Serializable]
public class Notification
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool HasBeenShown { get; set; }
    public bool HasBeenDone { get; set; }
    public string Text { get; set; }


    public Notification() { }

    public Notification(int id, string name, bool hasBeenShown, bool hasBeenDone, string text)
    {
        Id = id;
        Name = name;
        HasBeenShown = hasBeenShown;
        HasBeenDone = hasBeenDone;
        Text = text;
    }
}
