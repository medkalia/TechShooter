using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour {

    public float lerpSpeed = 2f;

    private Player player;
    [SerializeField]
    private Image content;
    [SerializeField]
    private Text valueText;
    [SerializeField]
    private Color lowColor;
    [SerializeField]
    private Color fullColor;

    private float fillAmount;

    void Start () {
        player = Player.Instance;

    }
	
	void Update () {
        HandleBar();
	}

    private void HandleBar()
    {
        
        valueText.text = "Health : " + Mathf.Clamp(player.playerStats.currentHealth, 0, player.playerStats.baseHealth);
        fillAmount = Mathf.Lerp(content.fillAmount, MathfUtil.Map(player.playerStats.currentHealth, 0, player.playerStats.baseHealth, 0, 1), Time.deltaTime * lerpSpeed);
        content.fillAmount = fillAmount;
        content.color = Color.Lerp(lowColor, fullColor, fillAmount);

    }
}
