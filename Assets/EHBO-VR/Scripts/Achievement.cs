using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    [Header("Feedback Settings")]
    [SerializeField] private TMP_Text achievementText;
    [SerializeField] private Animator animator;

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
                achievementText.text = "Achievement Unlocked!\nTimer onder controle";
                break;
            case 2:
                achievementText.text = "Achievement Unlocked!\nVR Handen Meester";
                break;
            case 3:
                achievementText.text = "Achievement Unlocked!\nHandgebaren Expert";
                break;
            default:
                return;
        }

        // Speel de animatie vanaf het begin
        animator.Play("Achievement", 0, 0f);
    }
}
