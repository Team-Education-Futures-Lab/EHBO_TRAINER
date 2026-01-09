using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    [Header("Feedback Settings")]
    [SerializeField] private TMP_Text achievementText;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource achievementSound;

    // Houd bij welke achievements al getoond zijn
    private HashSet<int> unlockedAchievementsList = new HashSet<int>();

    public void UnlockAchievement(int completedStep)
    {
        if (achievementText == null || animator == null)
            return;

        // Check of deze achievement al is vrijgespeeld
        if (unlockedAchievementsList.Contains(completedStep))
            return; 

        // Voeg toe aan de lijst van unlocks
        unlockedAchievementsList.Add(completedStep);

        switch (completedStep)
        {
            case 1:
                achievementText.text =
                "<size=130%><b>De tijd tikt</b></size>\n" +
                "<size=90%>Prestatie behaald</size>\n" +
                "TIjdsbalk onder controle";
                break;

            case 2:
                achievementText.text =
                "<size=130%><b>Aan de slag</b></size>\n" +
                "<size=90%>Prestatie behaald</size>\n" +
                "VR-handen onder controle";
                break;

            case 3:
                achievementText.text =
                "<size=130%><b>EHBO in de praktijk</b></size>\n" +
                "<size=90%>Prestatie behaald</size>\n" +
                "Handgebaren onder controle";
                break;

            default:
                return;
        }

        // Speel de animatie vanaf het begin
        animator.Play("Achievement", 0, 0f);

        if (achievementSound != null)
            achievementSound.Play();
    }
}
