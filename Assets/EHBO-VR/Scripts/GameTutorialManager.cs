using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameTutorialManager : MonoBehaviour
{
    public TMP_Text[] tutorialTexts;
    private int currentStep = 0;
    private int maxSteps = 4;

    public RectTransform timerCanvas;

    public TimeManager timeManager;

    public Achievement achievementManager;

    [Header("Video Player objects")]
    public GameObject videoPlayerHandGestures;
    public GameObject videoPlayerVRHands;

    [Header("To the Game Button")]
    public GameObject toGameButton;

    [Header("Cube Grab Display")]
    public GameObject CubeGrabDisplay;
    public GameObject Cube;

    void Start()
    {
        StartCoroutine(InitTutorial());

        videoPlayerHandGestures.SetActive(false);
        videoPlayerVRHands.SetActive(false);
        toGameButton.SetActive(false);
        CubeGrabDisplay.SetActive(false);
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
                tutorialTexts[1].text = "Welkom bij de EHBO VR tutorial!";
                tutorialTexts[2].text = "In deze tutorial worden 3 basis functies uitgelegd.";
                timeManager.ResetTimer(0f);
                timerCanvas.gameObject.SetActive(false);
                break;
            case 1:
                tutorialTexts[0].text = "1/3";
                tutorialTexts[1].text = "Timer introductie";
                tutorialTexts[2].text = "De timer geeft aan hoe lang je nog hebt om het slachtoffer te redden.";
                timeManager.ResetTimer(15f);
                timerCanvas.gameObject.SetActive(true);
                videoPlayerVRHands.SetActive(false);
                CubeGrabDisplay.SetActive(false);
                break;
            case 2:
                tutorialTexts[0].text = "2/3";
                tutorialTexts[1].text = "VR handen introductie";
                tutorialTexts[2].text = "Tijdens de game moet je een aantal handelingen uitvoeren.\n\n" + "Deze voer je uit met je VR-handen.";


                timeManager.ResetTimer(0f);
                timerCanvas.gameObject.SetActive(false);
                videoPlayerHandGestures.SetActive(false);
                videoPlayerVRHands.SetActive(true);
                CubeGrabDisplay.SetActive(true);
                ResetCube();
                break;
            case 3:
                tutorialTexts[0].text = "3/3";
                tutorialTexts[1].text = "VR hand gebaren introductie";
                tutorialTexts[2].text = "Je zult een aantal hand gebaren moeten uitvoeren.";
                videoPlayerHandGestures.SetActive(true);
                videoPlayerVRHands.SetActive(false);
                toGameButton.SetActive(false);
                CubeGrabDisplay.SetActive(false);
                break;
            case 4:
                tutorialTexts[0].text = "";
                tutorialTexts[1].text = "Tutorial voltooid!";
                tutorialTexts[2].text = "Je kunt nu verder met de game.\n\n" + "Klik op de rechterknop 'Naar de game'.";
                videoPlayerHandGestures.SetActive(false);
                toGameButton.SetActive(true);
                break;
        }
    }
    private void ResetCube()
    {
        Cube.transform.localPosition = new Vector3(-0.0469999984f, 0.0860000029f, 0f);
        Cube.transform.localRotation = Quaternion.identity;

        // (optioneel maar belangrijk bij VR physics)
        Rigidbody rb = Cube.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
