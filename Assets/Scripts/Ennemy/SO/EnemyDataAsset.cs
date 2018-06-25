#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class EnemyDataAsset
{
    [MenuItem("Assets/Create/EnemyData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<EnemyData>();
    }
}
#endif