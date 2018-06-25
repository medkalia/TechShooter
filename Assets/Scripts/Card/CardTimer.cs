using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTimer : MonoBehaviour {

    [Tooltip("Timer's Card image container")]
    public Image image;
    [Tooltip("Timer's CountDown text")]
    public Text countDownText;
    [Tooltip("Timer's CountDown text low color")]
    public Color lowTextColor;
    [Tooltip("Timer's CountDown text full color")]
    public Color fullTextColor;

    private void Start()
    {
        
    }

    public void SetTimer (Card card)
    {
        image.sprite = Resources.Load<Sprite>(card.MicroImageName);
        StartCoroutine(StartCountDown(card.EffectDuration));
    }

    public IEnumerator StartCountDown(float duration)
    {
        float timeLeft = duration;
        float percentage = 100f;
        while (timeLeft > 0)
        {
            string temp = string.Format("{0:0.0}", timeLeft);
            countDownText.text = temp;
            percentage = MathfUtil.Map(timeLeft, 0, duration, 0, 1);
            countDownText.color = Color.Lerp(lowTextColor, fullTextColor, percentage);
            timeLeft -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
            
        }
        Destroy(gameObject);
    }
}
