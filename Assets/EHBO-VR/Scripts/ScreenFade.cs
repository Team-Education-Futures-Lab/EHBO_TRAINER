using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage; // Fullscreen Image voor fade
    public float fadeDuration = 2f; // Tijd in seconden voor fade

    
    public void StartScreenFade()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
                
        yield return StartCoroutine(Fade(0f, 1f)); // Fade out (van transparant naar zwart)
        
        yield return new WaitForSeconds(1f);// Wacht eventueel even in volledig zichtbaar (optioneel)

        yield return StartCoroutine(Fade(1f, 0f));// Fade in (van zwart naar transparant)
       
        Debug.Log("Fade In and Out completed.");
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Zorg dat het einde exact wordt ingesteld
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
