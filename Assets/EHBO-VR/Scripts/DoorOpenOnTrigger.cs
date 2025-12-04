using UnityEngine;

public class DoorOpenOnNPC : MonoBehaviour
{
    public Animator doorAnimator; // Animator van de deur

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPCPlayer") && doorAnimator != null)
        {
            doorAnimator.enabled = true;
            Debug.Log("NPCPlayer trigger detected, opening door.");
        }
    }
}
