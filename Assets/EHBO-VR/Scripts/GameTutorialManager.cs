using System.Collections;
using TMPro;
using UnityEngine;

public class GameTutorialManager : MonoBehaviour
{
    public TMP_Text[] tutorialTexts;
    private int currentStep = 0;
    private int maxSteps = 4;

    public RectTransform timerCanvas;

    public TimeManager timeManager;

    public Achievement achievementManager;


    void Start()
    {
        StartCoroutine(InitTutorial());

        
    }

    private IEnumerator InitTutorial()
    {
        // Wacht 1 frame zodat TimeManager zijn Start() heeft uitgevoerd
        yield return null;

        UpdateTexts();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            NextStep();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PreviousStep();
        }
    }

    public void NextStep()
    {
        if (currentStep < maxSteps)
        {
            int completedStep = currentStep; // stap die je NET hebt geleerd

            currentStep++;
            UpdateTexts();
            Debug.Log("Next Step " + currentStep);


            achievementManager.UnlockAchievement(completedStep);
        }

    }

    public void PreviousStep()
    {
        if (currentStep > 0)
        {
            currentStep--;
            UpdateTexts();
            Debug.Log("Previous Step " + currentStep);
        }
    }


    private void UpdateTexts()
    {
      
      

        switch (currentStep)
        {
            case 0:
                tutorialTexts[0].text = "0/3";
                tutorialTexts[1].text = "Welkom bij de EHBO VR training!";
                tutorialTexts[2].text = "In deze tutorial zullen we je basis functies uitleggen van de game.";
                timeManager.ResetTimer(0f);
                timerCanvas.gameObject.SetActive(false);
                break;
            case 1:
                tutorialTexts[0].text = "1/3";
                tutorialTexts[1].text = "Timer introductie";
                tutorialTexts[2].text = "De timer geeft aan hoe lang je nog hebt om het slachtoffer te redden in de game.";
                timeManager.ResetTimer(15f);
                timerCanvas.gameObject.SetActive(true);
                break;

            case 2:
                tutorialTexts[0].text = "2/3";
                tutorialTexts[1].text = "VR handen introductie";
                tutorialTexts[2].text = "Tijdens het reanimeren zul je een aantal handelingen moeten uitvoeren. Deze handelingen zul je kunnen doen met je handen in VR.";
                timeManager.ResetTimer(0f);
                timerCanvas.gameObject.SetActive(false);
                break;

            case 3:
                tutorialTexts[0].text = "3/3";
                tutorialTexts[1].text = "VR hand gebaren introductie";
                tutorialTexts[2].text = "Je zult een aantal hand gestures moeten gebruiken tijdens het scenario.";
                break;
            case 4:
                tutorialTexts[0].text = "";
                tutorialTexts[1].text = "Tutorial voltooid!";
                tutorialTexts[2].text = "je kunt nu door naar de game door te klikken op de knop";
                break;
        }
    }
}
