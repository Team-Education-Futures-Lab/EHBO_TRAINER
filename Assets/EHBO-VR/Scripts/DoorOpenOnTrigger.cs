using UnityEngine;

public class DoorOpenOnNPC : MonoBehaviour
{
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPCPlayer") && doorAnimator != null)
        {
            doorAnimator.SetTrigger("OpenDoor");
            Debug.Log("NPCPlayer trigger detected, opening door.");
        }
    }
}
