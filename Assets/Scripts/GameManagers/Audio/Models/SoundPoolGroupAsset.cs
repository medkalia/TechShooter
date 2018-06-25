#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class SoundPoolGroupAsset
{
    [MenuItem("Assets/Create/Sound/Group")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SoundPoolGroup>();
    }
}
#endif