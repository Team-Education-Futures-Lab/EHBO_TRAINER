using TMPro;
using UnityEngine;

public class GameTutorialManager : MonoBehaviour
{
    public TMP_Text[] tutorialTexts;
    private int currentStep = 0;
    private int maxSteps = 2; // 0, 1, 2

    void Start()
    {
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
            currentStep++;
            UpdateTexts();
            Debug.Log("Next Step " + currentStep);
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
                tutorialTexts[0].text = "1/3";
                tutorialTexts[1].text = "Timer introductie";
                tutorialTexts[2].text = "";
                break;

            case 1:
                tutorialTexts[0].text = "2/3";
                tutorialTexts[1].text = "VR hands introductie";
                tutorialTexts[2].text = "";
                break;

            case 2:
                tutorialTexts[0].text = "3/3";
                tutorialTexts[1].text = "VR hand gestures introductie";
                tutorialTexts[2].text = "";
                break;
        }
    }
}
