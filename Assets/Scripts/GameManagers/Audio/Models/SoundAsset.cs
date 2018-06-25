#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class SoundAsset
{
    [MenuItem("Assets/Create/Sound/Sound")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Sound>();
    }
}
#endif