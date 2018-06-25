using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldBar : MonoBehaviour
{

    [SerializeField]
    private Text valueText;

    private int currentLevelGoldCount = 0;

    // Use this for initialization
    void Start()
    {
        valueText.text = DataController.Instance.GetProgressService().playerProgress.Gold.ToString();
        DataController.Instance.GetProgressService().levelGoldCount = 0;
    }

    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if (!(currentLevelGoldCount == DataController.Instance.GetProgressService().levelGoldCount))
        {
            int temp = Convert.ToInt16(valueText.text);
            DataController.Instance.GetProgressService().levelGoldCount++;
            currentLevelGoldCount = DataController.Instance.GetProgressService().levelGoldCount;
            valueText.text =(++temp).ToString() ;
            GetComponentInChildren<TextPulsator>().PulseText();
        }
        
    }
}
