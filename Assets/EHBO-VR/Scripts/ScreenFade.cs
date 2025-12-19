using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;           // Fullscreen Image voor fade
    public float fadeDuration = 2f;   // Tijd in seconden voor fade
    public Transform cameraRig;      // Empty object dat de VR camera bevat
    public Transform targetPosition;  // Plaats waar je camera naartoe moet

    public void StartScreenFade()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        // Fade out: transparant -> zwart
        yield return StartCoroutine(Fade(0f, 1f));

        // **Scherm is zwart, verplaats nu de camera root**
        if (cameraRig != null && targetPosition != null)
        {
            cameraRig.position = targetPosition.position;
            cameraRig.rotation = targetPosition.rotation;
        }

        // Optioneel: even wachten op zwart scherm
        yield return new WaitForSeconds(0.5f);

        // Fade in: zwart -> transparant
        yield return StartCoroutine(Fade(1f, 0f));

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
