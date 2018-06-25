using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    public float lerpSpeed = 2f;

    [SerializeField]
    private Image content;
    [SerializeField]
    private Text valueText;
    [SerializeField]
    private Color lowColor;
    [SerializeField]
    private Color fullColor;

    private float fillAmount;
    private Enemy enemy;

    //private float currentEnemyFacingDirection;

    void Start()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
        //currentEnemyFacingDirection = enemy.enemyMovementInfo.FacingDirection;
    }

    void Update()
    {
        gameObject.GetComponentInParent<Canvas>().enabled = true;
        HandleBar();
    }

    private void HandleBar()
    {
        //if (currentEnemyFacingDirection != enemy.enemyMovementInfo.FacingDirection)
        //    Debug.Log("changednadhr");
        //GameObject canvas = transform.parent.gameObject;
        //canvas.GetComponent<RectTransform>().localScale = new Vector2 (Mathf.Abs(canvas.GetComponent<RectTransform>().localScale.x), canvas.GetComponent<RectTransform>().localScale.y) ;
        valueText.text = Mathf.Clamp(enemy.enemyStats.currentHealth, 0, enemy.enemyStats.baseHealth).ToString();
        fillAmount = Mathf.Lerp(content.fillAmount, MathfUtil.Map(enemy.enemyStats.currentHealth, 0, enemy.enemyStats.baseHealth, 0, 1), Time.deltaTime * lerpSpeed);
        content.fillAmount = fillAmount;
        content.color = Color.Lerp(lowColor, fullColor, fillAmount);
    }
}
