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


    [Header("Voice-over Settings")]
    [SerializeField] private GameObject voiceOverManager;

    [SerializeField]
    private Achievement achievementManager;



    private AudioSource audioSource;

    void Start()
    {
        InitializeTasks();

        // Ensure there is an AudioSource on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();


        
    }

    public void StartVRTutorialVoiceOver()
    {
        // Play the voice-over for the first task
        StartCoroutine(PlayVoiceOverWithDelay(tasks[0].voiceOver, 4f));
    }


    // Initialize every task (only the first one is revealed immediately)
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

    // Called by other scripts to register that a task is complete
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

    // Completes the current task, provides feedback, and reveals the next one
    private void CompleteCurrentTask()
    {
        Task currentTask = tasks[currentTaskIndex];

        // Retrieve the VoiceOver component once
        VoiceOver voiceOverComponent = voiceOverManager.GetComponent<VoiceOver>();
        if (voiceOverComponent != null)
        {
            // Stop the currently playing voice-over
            voiceOverComponent.StopVoiceOver();
        }

        // Show achievement for completing the task
        if (achievementManager != null)
        {
            achievementManager.UnlockAchievement(Achievement.TutorialType.VR, currentTaskIndex + 1);
        }

        // Update the task image to show the completed texture
        if (currentTask.completedTexture != null)
            currentTask.taskImage.texture = currentTask.completedTexture;


        // Reveal the next task (if available)
        currentTaskIndex++;
        if (currentTaskIndex < tasks.Count)
        {
            Task nextTask = tasks[currentTaskIndex];
            nextTask.taskText.text = nextTask.taskName;

            // Play the next task voice-over with a short delay
            if (voiceOverComponent != null)
            {
                StartCoroutine(PlayVoiceOverWithDelay(nextTask.voiceOver, 2f));
            }
        }
        else
        {
            Debug.Log("All tasks completed!");
        }
    }


    
    private IEnumerator PlayVoiceOverWithDelay(AudioClip clip, float delay)
    {
        if (clip == null || voiceOverManager == null)
            yield break;

        yield return new WaitForSeconds(delay);

        VoiceOver voiceOverComponent = voiceOverManager.GetComponent<VoiceOver>();
        if (voiceOverComponent != null)
            voiceOverComponent.PlayVoiceOver(clip);
    }



}
