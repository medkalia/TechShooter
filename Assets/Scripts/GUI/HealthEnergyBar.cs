using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthEnergyBar : MonoBehaviour {

    public float lerpSpeed = 2f;

    private Player player;
    [Space]
    [Header("Health")]
    [SerializeField]
    private Image contentImageHp;
    [SerializeField]
    private Text valueTextHp;
    [SerializeField]
    private Color lowColor;
    [SerializeField]
    private Color fullColor;

    [Space]
    [Header("TechPoints")]
    [SerializeField]
    private Image contentImageTP;
    [SerializeField]
    private Text valueTextTp;

    private float fillAmountHp;
    private float fillAmountTp;

    void Start () {
        player = Player.Instance;

    }
	
	void Update () {
        HandleBar();
	}

    private void HandleBar()
    {
        //***HP
        valueTextHp.text = Mathf.Clamp(player.playerStats.currentHealth, 0, player.playerStats.baseHealth).ToString();
        fillAmountHp = Mathf.Lerp(contentImageHp.fillAmount, Map(player.playerStats.currentHealth, 0, player.playerStats.baseHealth, 0, 1), Time.deltaTime * lerpSpeed);
        contentImageHp.fillAmount = fillAmountHp;
        contentImageHp.color = Color.Lerp(lowColor, fullColor, fillAmountHp);

        //***TP
        valueTextTp.text = Mathf.Clamp(player.playerStats.currentTechPoints, 0, player.playerStats.maxTechPoints).ToString();
        fillAmountTp = Mathf.Lerp(contentImageTP.fillAmount, Map(player.playerStats.currentTechPoints, 0, player.playerStats.maxTechPoints, 0, 1), Time.deltaTime * lerpSpeed);
        contentImageTP.fillAmount = fillAmountTp;
    }

    private float Map (float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
