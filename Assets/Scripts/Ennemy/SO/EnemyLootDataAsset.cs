#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class EnemyLootDataAsset
{
    [MenuItem("Assets/Create/EnemyLootData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<EnemyLootData>();
    }
}
#endif