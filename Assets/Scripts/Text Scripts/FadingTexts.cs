using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingTexts : MonoBehaviour
{
    [SerializeField] private Text[] texts;
    public float fadeDuration = 1.0f;
    public float delayBetweenTexts = 0.5f;


    private void Start()
    {
        // Set the initial alpha value of all texts to 0
        foreach (Text text in texts)
        {
            Color color = text.color;
            color.a = 0.0f;
            text.color = color;
        }

        FadeTexts();
    }

    public void FadeTexts()
    {
        StartCoroutine(FadeTextsCoroutine());
    }

    private IEnumerator FadeTextsCoroutine()
    {
        foreach (Text text in texts)
        {
            yield return new WaitForSeconds(delayBetweenTexts);

            float timer = 0.0f;

            // Set the initial alpha value of the text to 0
            Color color = text.color;
            color.a = 0.0f;
            text.color = color;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;

                // Gradually increase the alpha value of the text color over time
                float alpha = Mathf.Lerp(0.0f, 1.0f, timer / fadeDuration);

                color = text.color;
                color.a = alpha;
                text.color = color;

                yield return null;
            }

            // Ensure that the final alpha value is exactly 1
            color = text.color;
            color.a = 1.0f;
            text.color = color;
        }
    }
}
