using UnityEngine;

public class DoorOpenOnNPC : MonoBehaviour
{
    public Animator doorAnimator;
    public AudioSource doorSound; // voeg hier het geluid toe via Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPCPlayer") && doorAnimator != null)
        {
            doorAnimator.SetTrigger("OpenDoor");
            Debug.Log("NPCPlayer trigger detected, opening door.");

            // speel het geluid af
            if (doorSound != null)
                doorSound.Play();
        }
    }
}
