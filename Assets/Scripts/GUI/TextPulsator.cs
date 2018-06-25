using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPulsator : MonoBehaviour {

    public AnimationCurve scaleCurve;
    public float duration = 1f;

    private Text valueText;


    private void Awake()
    {
        valueText = GetComponent<Text>();
    }

    public void PulseText()
    {
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse ()
    {
        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time += Time.deltaTime / duration;
            valueText.rectTransform.localScale = new Vector3(scale, scale, 1);
            yield return new WaitForFixedUpdate();
        }
    }
}
