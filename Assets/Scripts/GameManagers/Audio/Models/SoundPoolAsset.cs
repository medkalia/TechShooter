#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class SoundPoolAsset
{
    [MenuItem("Assets/Create/Sound/Pool")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SoundPool>();
    }
}
#endif