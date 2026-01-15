using UnityEngine;

public class DoorOpenOnTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator doorAnimator;
    [SerializeField]
    private AudioSource doorSound; // Assign the door audio clip in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPCPlayer") && doorAnimator != null)
        {
            doorAnimator.SetTrigger("OpenDoor");
            Debug.Log("NPCPlayer trigger detected, opening door.");

            if (doorSound != null)
                doorSound.Play();
        }
    }
}
