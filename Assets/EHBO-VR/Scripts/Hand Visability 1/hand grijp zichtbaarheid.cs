using System.Collections.Generic;
using UnityEngine;

public class handgrijpzichtbaarheid : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToMakeVisable; // Object to show when the player is grabbing
    [SerializeField] private bool isGrabbing = false; // Determines if the object should be visible

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isGrabbing)
        {
            // Make the object visible immediately
            ObjectToMakeVisable.SetActive(true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isGrabbing)
        {
            // No action needed; the object stays hidden until grabbing is enabled
        }
        if (other.CompareTag("Player") && isGrabbing)
        {
            ObjectToMakeVisable.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the object immediately when the player exits
            ObjectToMakeVisable.SetActive(false);
        }
    }

    // Optional methods to control the isGrabbing flag
    public void EnableGrabbing()
    {
        isGrabbing = true;
    }

    public void DisableGrabbing()
    {
        isGrabbing = false;
    }
}
