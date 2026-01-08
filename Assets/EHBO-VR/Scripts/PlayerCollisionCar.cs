using Oculus.Interaction.Locomotion;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionCar : MonoBehaviour
{
    private bool gotHit = false;
    public TextMeshProUGUI hitText;

    public FirstPersonLocomotor locomotor;

    // Voeg hier je crash-geluid toe in de Inspector
    public AudioSource crashSound;

    private void Start()
    {
        hitText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && !gotHit)
        {
            Debug.Log("Je bent aangereden door een auto: " + other.gameObject.name);
            gotHit = true;
            hitText.enabled = true;

            // Crash-geluid afspelen
            if (crashSound != null)
                crashSound.Play();

            locomotor.enabled = false;

            Animator carAnimator = other.GetComponent<Animator>();
            if (carAnimator != null)
            {
                carAnimator.enabled = false;
            }

            StartCoroutine(RestartGameAfterDelay(4f));
        }
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        Debug.Log("Game wordt opnieuw gestart over " + delay + " seconden...");
        yield return new WaitForSecondsRealtime(delay);

        // Huidige scene herladen
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
