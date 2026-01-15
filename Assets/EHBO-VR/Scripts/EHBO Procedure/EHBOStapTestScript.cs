using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHBOStapTestScript : MonoBehaviour
{
    // Reference to the main ExerciseTracker script
    [SerializeField]
    private EHBOStappenChecker StappenTracker;

    // Customizable step name and required duration, assignable in the Inspector
    [SerializeField] private string stepName = "Default Step";
    [SerializeField] private float requiredDuration = 2.0f; // Duration in seconds

    // Internal timer to track how long the action is performed
    private float elapsedActionTime = 0.0f;
    private bool isCountingActionTime = false;

    // Method to start counting when the action begins

    // Method to call when this step is completed
    public void CompleteStep()
    {
        StappenTracker.RegisterStep(stepName);
        Debug.Log($"Step '{stepName}' completed after holding for {requiredDuration} seconds.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            isCountingActionTime = true;  // Start counting
        }
    }

    // Method to reset if the action is interrupted
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCountingActionTime = false;
            elapsedActionTime = 0.0f;  // Reset timer if the action stops
        }
    }

    void Update()
    {
        // Only count time if the action is being performed
        if (isCountingActionTime)
        {
            elapsedActionTime += Time.deltaTime;

            // Check if the required duration is reached
            if (elapsedActionTime >= requiredDuration)
            {
                CompleteStep();
                isCountingActionTime = false;  // Stop counting after completion
            }
        }
    }
}
