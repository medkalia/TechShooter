#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class PlayerDataAsset
{
    [MenuItem("Assets/Create/PlayerData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<PlayerData>();
    }
}
#endif