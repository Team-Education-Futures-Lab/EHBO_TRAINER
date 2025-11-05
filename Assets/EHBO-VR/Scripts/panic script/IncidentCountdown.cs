using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IncidentCountdown : MonoBehaviour
{
    // Variables for countdown and control
    public bool IsActive = false;
    public float countdownTime = 5f; // Default countdown time
    public bool useRandomTime = false; // Toggle for random countdown time
    public float randomMin = 3f; // Minimum random countdown time
    public float randomMax = 10f; // Maximum random countdown time

    public Animator[] objectsToAnimate; // Reference to the animators
    public AudioSource countdownEndSound; // Reference to the AudioSource for the sound effect

    public GameObject[] responders; // De 2 NPC's die moeten reageren

    private float currentTime;
    private bool countdownFinished = false; // To track if the countdown is already finished

    void Start()
    {
        // Set countdown time
        if (useRandomTime)
        {
            countdownTime = Random.Range(randomMin, randomMax);
        }

        currentTime = countdownTime;
    }

    void Update()
    {
        if (IsActive && !countdownFinished)
        {
            // Reduce countdown timer
            currentTime -= Time.deltaTime;

            // Check if countdown has finished
            if (currentTime <= 0f)
            {
                ActivateAction();
            }
        }
    }

    void ActivateAction()
    {
        countdownFinished = true; // Mark that countdown is finished

        // Trigger the animation on all specified objects
        SwapAnimation();
        ActivateResponders();

        // Find and stop all NavMeshAgents in the scene by setting their speed to 0
        NavMeshAgent[] navAgents = FindObjectsOfType<NavMeshAgent>();
        foreach (NavMeshAgent agent in navAgents)
        {
            agent.speed = 0; // Set speed to 0 to stop the agents
        }

        // Play the sound effect only when the countdown finishes
        if (countdownEndSound != null)
        {
            countdownEndSound.Play();
        }

  
    }

    // Call this method to start the countdown
    public void Activate()
    {
        IsActive = true;
        currentTime = countdownTime; // Reset countdown time when activated
        countdownFinished = false;  // Reset the countdown finished flag
    }

    // Swap animation on the specified animator objects
    public void SwapAnimation()
    {
        foreach (Animator animator in objectsToAnimate)
        {
            if (animator != null)
            {
                animator.SetBool("shocked", true); // Set the 'shocked' animation parameter to true
            }
        }
    }

    public void ActivateResponders()
    {
        StartCoroutine(ActivateRespondersWithDelay(3f)); // ← 2 seconden wachten
    }

    private IEnumerator ActivateRespondersWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wacht de opgegeven tijd

        foreach (GameObject responder in responders)
        {
            if (responder != null)
            {
                var movement = responder.GetComponent<NPCMovement>();
                if (movement != null)
                {
                    movement.enabled = true; // Zet hun movement script aan
                }
            }
        }
    }
}
