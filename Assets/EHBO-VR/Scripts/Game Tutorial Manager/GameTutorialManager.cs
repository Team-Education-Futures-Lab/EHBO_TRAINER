using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameTutorialManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] tutorialTexts;
    private int currentStep = 0;
    private int maxSteps = 4;

    [SerializeField]
    private RectTransform timerCanvas;

    [SerializeField]
    private TimeManager timeManager;

    [SerializeField]
    private Achievement achievementManager;

    [Header("Video Player objects")]
    [SerializeField]
    private GameObject videoPlayerHandGestures;
    [SerializeField]
    private GameObject videoPlayerVRHands;

    [Header("To the Game Button")]
    [SerializeField]
    private GameObject toGameButton;

    [Header("Cube Grab Display")]
    [SerializeField]
    private GameObject CubeGrabDisplay;
    [SerializeField]
    private GameObject Cube;

    [Header("Voice-over per step")]
    [SerializeField] private AudioClip[] VoiceOvers; // 0 = stap 0, 1 = stap 1, etc.
    [SerializeField] private VoiceOver voiceOverManager; // verwijzing naar je bestaande VoiceOver manager
    [SerializeField] private float voiceOverDelay = 2f;   // optioneel: delay voordat voice over begint

    private bool tutorialStarted = false;


    void Start()
    {  

        videoPlayerHandGestures.SetActive(false);
        videoPlayerVRHands.SetActive(false);
        toGameButton.SetActive(false);
        CubeGrabDisplay.SetActive(false);
    }
    public void StartGameTutorial()
    {
        if (tutorialStarted)
            return;

        tutorialStarted = true;
        currentStep = 0;

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
            currentStep++;       // eerst de stap omhoog
            UpdateTexts();       // update teksten

            // Geef alleen achievements door vanaf stap 2
            if (currentStep > 1) // stap 0 en 1 geen achievement
            {
                achievementManager.UnlockAchievement(Achievement.TutorialType.Game, currentStep - 1);
                // currentStep - 1 = stap die je net hebt voltooid
            }

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
                tutorialTexts[0].text = "0/3";
                tutorialTexts[1].text = "Welkom bij de EHBO VR tutorial!";
                tutorialTexts[2].text = "In deze tutorial worden 3 basis functies uitgelegd.\n- De tijdsbalk\n- VR handen\n- Hand gebaren";
                timeManager.ResetTimer(0f);
                timerCanvas.gameObject.SetActive(false);
                break;
            case 1:
                tutorialTexts[0].text = "1/3";
                tutorialTexts[1].text = "Tijdsbalk introductie";
                tutorialTexts[2].text = "De tijdsbalk geeft aan hoe lang je nog hebt om het slachtoffer te redden.\n\nBij een reanimatie is dit bijvoorbeeld 4 minuten.";
                timeManager.ResetTimer(23f);
                timerCanvas.gameObject.SetActive(true);
                videoPlayerVRHands.SetActive(false);
                CubeGrabDisplay.SetActive(false);
                break;
            case 2:
                tutorialTexts[0].text = "2/3";
                tutorialTexts[1].text = "VR handen introductie";
                tutorialTexts[2].text = "Tijdens de game moet je een aantal handelingen uitvoeren.\n\nDeze voer je uit met je VR-handen.";


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
                tutorialTexts[2].text = "Je zult een aantal hand gebaren moeten uitvoeren.\n\nHier op het bord zie je op welke manier je dit gaat kunnen doen.";
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

        if (tutorialStarted)
        {
            PlayGameTutorialVoiceOver();
        }
    }
    public void PlayGameTutorialVoiceOver()
    {
        if (voiceOverManager == null || VoiceOvers == null)
            return;

        int index = Mathf.Clamp(currentStep, 0, VoiceOvers.Length - 1); // safe check
        AudioClip clipToPlay = VoiceOvers[index];

        if (clipToPlay != null)
        {
            // Stop eventueel huidige voice-over
            voiceOverManager.StopVoiceOver();

            // Start de nieuwe met delay
            StartCoroutine(PlayVoiceOverWithDelay(clipToPlay, voiceOverDelay));
        }
    }

    private IEnumerator PlayVoiceOverWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        voiceOverManager.PlayVoiceOver(clip);
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
