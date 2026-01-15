using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptbasisdetectie : MonoBehaviour
{
    [SerializeField] private float requiredDuration = 2.0f;
    [SerializeField] private clipboard clipboardTasks;
    [SerializeField] private string taskToComplete;

    [Header("Trigger Settings")]
    [SerializeField] private List<GameObject> objectsToDeActivateOnEnter;
    [SerializeField] private List<GameObject> objectsToActivateOnExit;

    private float elapsedActionTime = 0.0f;
    private bool isCountingActionTime = false;
    private BoxCollider triggerCollider;

    void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCountingActionTime = true;

            if (triggerCollider != null)
                triggerCollider.size *= 3f;

            foreach (GameObject obj in objectsToDeActivateOnEnter)
                obj.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCountingActionTime = false;
            elapsedActionTime = 0.0f;

            if (triggerCollider != null)
                triggerCollider.size /= 3f;

            foreach (GameObject obj in objectsToActivateOnExit)
                obj.SetActive(true);
        }
    }

    void Update()
    {
        if (isCountingActionTime)
        {
            elapsedActionTime += Time.deltaTime;

            if (elapsedActionTime >= requiredDuration)
            {
                CompleteTask();
                isCountingActionTime = false;
            }
        }
    }

    public void CompleteTask()
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
