using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealthBar : MonoBehaviour {

    public float lerpSpeed = 2f;

    [SerializeField]
    private Image content;
    [SerializeField]
    private Text valueText;
    
    private float fillAmount;
    private Enemy enemy;

    void Start()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
    }

    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {

        valueText.text = Mathf.Clamp(enemy.enemyStats.currentHealth, 0, enemy.enemyStats.baseHealth) + " / " + enemy.enemyStats.baseHealth;
        fillAmount = Mathf.Lerp(content.fillAmount, MathfUtil.Map(enemy.enemyStats.currentHealth, 0, enemy.enemyStats.baseHealth, 0, 1), Time.deltaTime * lerpSpeed);
        content.fillAmount = fillAmount;
    }

    
}
