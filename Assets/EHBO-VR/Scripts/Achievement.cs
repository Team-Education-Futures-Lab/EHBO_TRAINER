using System.Collections;
using TMPro;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    [Header("Feedback Settings")]
    [SerializeField] private AudioClip achievementSound;       // Geluid bij voltooien taak
    [SerializeField] private Canvas achievementCanvas;         // Canvas die kort getoond wordt
    [SerializeField] private float canvasDisplayDuration = 2f;// Tijd dat canvas zichtbaar blijft
    [SerializeField] private TMP_Text achievementText;

    void Start()
    {
        if (achievementCanvas != null)
        {
            achievementCanvas.gameObject.SetActive(false);
        }
            
    }

    public void UnlockAchievement(int completedStep)
    {
        if (achievementText == null || achievementCanvas == null)
            return;

        switch (completedStep)
        {
            case 1:
                achievementText.text = "Achievement Unlocked!\nTimer onder controle";
                break;

            case 2:
                achievementText.text = "Achievement Unlocked!\nVR Handen Meester";
                break;

            case 3:
                achievementText.text = "Achievement Unlocked!\nHandgebaren Expert";
                break;

            default:
                return; // Geen achievement voor andere stappen
        }

        StopAllCoroutines();
        StartCoroutine(ShowCanvasTemporarily());
    }

    private IEnumerator ShowCanvasTemporarily()
    {
        achievementCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(canvasDisplayDuration);
        achievementCanvas.gameObject.SetActive(false);
    }
}
