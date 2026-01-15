using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class BasisDetectieIphone : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private clipboard clipboardTasks;
    [SerializeField] private string taskToComplete;

    [Header("Trigger Settings")]
    [SerializeField] private List<GameObject> objectsToDeActivateOnEnter;
    [SerializeField] private List<GameObject> objectsToActivateOnExit;

    private bool taskCompleted = false;

    // DEZE methode koppel je in de UnityEvent
    public void OnInteract()
    {
        if (taskCompleted)
            return;

        CompleteTask();
        taskCompleted = true;

        foreach (GameObject obj in objectsToDeActivateOnEnter)
            obj.SetActive(false);

        foreach (GameObject obj in objectsToActivateOnExit)
            obj.SetActive(true);
    }

    private void CompleteTask()
    {
        if (clipboardTasks != null)
        {
            clipboardTasks.RegisterTaskCompletion(taskToComplete);
        }
        else
        {
            Debug.LogError("ClipboardTasks reference is not assigned in the inspector.");
        }
    }
}
