using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVisability : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToMakeVisable; // GameObject that toggles visibility when the player enters the trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectToMakeVisable.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectToMakeVisable.SetActive(false);
        }
    }
}
