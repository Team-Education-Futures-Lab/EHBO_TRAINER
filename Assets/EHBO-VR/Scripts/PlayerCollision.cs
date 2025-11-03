using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    private bool gotHit = false;
    public TextMeshProUGUI hitText;
   
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
