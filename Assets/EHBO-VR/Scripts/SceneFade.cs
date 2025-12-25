using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    private Image FadeScreen;

    private void Awake()
    {
        FadeScreen = GetComponent<Image>();
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        Color startColor = new Color(0, 0, 0, 1); // Zwart
        Color targetColor = new Color(0, 0, 0, 0); // Transparant
        yield return StartCoroutine(FadeCoroutine(startColor, targetColor, duration));

        gameObject.SetActive(false); // Schakel het fade scherm uit na het vervagen
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        gameObject.SetActive(true); // Zorg dat het fade scherm actief is
        Color startColor = new Color(0, 0, 0, 0); // Transparant
        Color targetColor = new Color(0, 0, 0, 1); // Zwart
        yield return StartCoroutine(FadeCoroutine(startColor, targetColor, duration));
    }

    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / duration;
            FadeScreen.color = Color.Lerp(startColor, targetColor, elapsedPercentage);

            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
