using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class ProjectileDataAsset
{
    [MenuItem("Assets/Create/ProjectileData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<ProjectileData>();
    }
}
#endif