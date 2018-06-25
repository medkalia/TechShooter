using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

    private void Start()
    {
        GetComponent<BIJ_UI_Module_LoadScene>().LoadScene("Main Menu");
    }
}
