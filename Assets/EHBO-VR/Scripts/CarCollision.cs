using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("CarCollision: auto heeft speler geraakt!");
        }
    }
}
