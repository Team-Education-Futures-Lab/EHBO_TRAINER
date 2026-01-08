using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class clipboard : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        [Tooltip("Name of the task")] public string taskName;
        [Tooltip("Image to display task state")] public RawImage taskImage;
        [Tooltip("Text for the task description")] public TextMeshProUGUI taskText;
        [Tooltip("Placeholder text for unrevealed tasks")] public string placeholderText;
        [Tooltip("Texture for completed task")] public Texture completedTexture;

        [Header("Voice-over")]
        public AudioClip voiceOver;
    }

    [Header("Task Settings")]
    [SerializeField] private List<Task> tasks = new List<Task>();
    private int currentTaskIndex = 0;

    [Header("Feedback Settings")]
    [SerializeField] private AudioClip completionSound;       // Geluid bij voltooien taak
    [SerializeField] private Canvas completionCanvas;         // Canvas die kort getoond wordt
    [SerializeField] private float canvasDisplayDuration = 2f;// Tijd dat canvas zichtbaar blijft

    [Header("Voice-over Settings")]
    [SerializeField] private GameObject voiceOverManager;



    private AudioSource audioSource;

    void Start()
    {
        InitializeTasks();

        // Zorg dat er een AudioSource op dit object zit
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Canvas standaard uitzetten
        if (completionCanvas != null)
            completionCanvas.gameObject.SetActive(false);
        
    }

    public void StartVRTutorialVoiceOver()
    {
        // Speel de voice over van de eerste taak

        StartCoroutine(PlayVoiceOverWithDelay(tasks[0].voiceOver, 4f));
    }


    // Initialiseer alle taken (enkel de eerste zichtbaar)
    private void InitializeTasks()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (i == 0)
                tasks[i].taskText.text = tasks[i].taskName;
            else
                tasks[i].taskText.text = tasks[i].placeholderText;
        }
    }

    // Wordt aangeroepen door andere scripts om een taak als voltooid te registreren
    public void RegisterTaskCompletion(string taskName)
    {
        if (currentTaskIndex < tasks.Count && tasks[currentTaskIndex].taskName == taskName)
        {
            CompleteCurrentTask();
        }
        else
        {
            Debug.Log($"Task '{taskName}' ignored. It is not the current task.");
        }
    }

    // Voltooit de huidige taak, geeft feedback en onthult de volgende
    private void CompleteCurrentTask()
    {
        Task currentTask = tasks[currentTaskIndex];

        // Haal VoiceOver component één keer op
        VoiceOver vo = voiceOverManager.GetComponent<VoiceOver>();
        if (vo != null)
        {
            // Stop huidige voice-over
            vo.StopVoiceOver();
        }

        // Update afbeelding naar completed texture
        if (currentTask.completedTexture != null)
            currentTask.taskImage.texture = currentTask.completedTexture;

        // Geef feedback aan speler
        PlayCompletionFeedback();

        // Ga door naar de volgende taak (indien beschikbaar)
        currentTaskIndex++;
        if (currentTaskIndex < tasks.Count)
        {
            Task nextTask = tasks[currentTaskIndex];
            nextTask.taskText.text = nextTask.taskName;

            // Speel de voice-over van de volgende taak met delay
            if (vo != null)
            {
                StartCoroutine(PlayVoiceOverWithDelay(nextTask.voiceOver, 2f));
            }
        }
        else
        {
            Debug.Log("All tasks completed!");
        }
    }





    // Feedback bij voltooien taak
    private void PlayCompletionFeedback()
    {
        // Geluid afspelen
        if (completionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(completionSound);
        }

        // Canvas tijdelijk tonen
        if (completionCanvas != null)
        {
            StopAllCoroutines(); // Zorg dat eerdere coroutines niet interfereren
            StartCoroutine(ShowCanvasTemporarily());
        }
    }

    private IEnumerator ShowCanvasTemporarily()
    {
        completionCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(canvasDisplayDuration);
        completionCanvas.gameObject.SetActive(false);
    }

    
    private IEnumerator PlayVoiceOverWithDelay(AudioClip clip, float delay)
    {
        if (clip == null || voiceOverManager == null)
            yield break;

        yield return new WaitForSeconds(delay);

        VoiceOver vo = voiceOverManager.GetComponent<VoiceOver>();
        if (vo != null)
            vo.PlayVoiceOver(clip);
    }



}