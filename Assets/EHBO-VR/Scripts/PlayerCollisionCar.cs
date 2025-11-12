using TMPro;
using UnityEngine;

public class PlayerCollisionCar : MonoBehaviour
{
    private bool gotHit = false;
    public TextMeshProUGUI hitText;

    public OVRPlayerController ovrPlayerController;

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

            ovrPlayerController.enabled = false;
            Time.timeScale = 0f;
        }
    }
}
