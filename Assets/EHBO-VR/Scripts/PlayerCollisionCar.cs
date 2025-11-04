using TMPro;
using UnityEngine;

public class PlayerCollisionCar : MonoBehaviour
{
    private bool gotHit = false;
    public TextMeshProUGUI hitText;

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

        }
    }
}
