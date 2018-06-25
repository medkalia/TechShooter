using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFlipper : MonoBehaviour {

    public AnimationCurve scaleCurve;
    public float duration = 0.5f;

    private Image image;
    private CardModel cardModel;

    private void Awake()
    {
        image = GetComponent<Image>();
        cardModel = GetComponent<CardModel>();
    }

    public void FlipCard(Sprite startImage, Sprite endImage)
    {
        StopCoroutine(Flip(startImage, endImage));
        StartCoroutine(Flip(startImage, endImage));
    }

    IEnumerator Flip (Sprite startImage, Sprite endImage)
    {
        Vector3 originalScale = transform.localScale;
        image.sprite = startImage;
        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time) * originalScale.x;
            time += Time.deltaTime / duration;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if (time >= 0.5f && image.sprite == startImage)
            {
                image.sprite = endImage;
            }

            yield return new WaitForFixedUpdate();
        }
        transform.localScale = originalScale;
    }
  
}
