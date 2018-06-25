using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour {

    public float lerpSpeed = 1f;

    private Player player;
    [SerializeField]
    private Image content;
    [SerializeField]
    private Text valueText;

    

    void Start () {
        player = Player.Instance;

    }
	
	void Update () {
        HandleBar();
	}

    private void HandleBar()
    {
        content.fillAmount =Mathf.Lerp(content.fillAmount, Map(player.playerStats.currentTechPoints, 0, player.playerStats.maxTechPoints, 0, 1), Time.deltaTime * lerpSpeed);

        valueText.text = player.playerStats.currentTechPoints + " / " + player.playerStats.maxTechPoints;
    }

    private float Map (float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
