using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BIJ_UI_Module_QuitApplication : MonoBehaviour {

    #region Variables
    #endregion

    #region Main Methods
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
		Application.Quit();
        #endif
    }
    #endregion

    #region Helper Methods
    #endregion
}
