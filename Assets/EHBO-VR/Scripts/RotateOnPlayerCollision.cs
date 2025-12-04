using UnityEngine;

public class RotateOnPlayerCollision : MonoBehaviour
{
    public string targetTag = "NPCPlayer";   // Tag van je NPC
    public float openAngle = 90f;      // Hoe ver de deur draait
    public float rotationSpeed = 120f; // Graden per seconde



    private bool isOpening = false;
    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = transform.rotation; // Beginstand
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.tag);

        if (other.CompareTag(targetTag))
        {
            Debug.Log("NPCPlayer entered trigger, starting door rotation.");
            targetRotation *= Quaternion.Euler(0f, openAngle, 0f);
            isOpening = true;
        }
    }


    private void Update()
    {
        if (isOpening)
        {
            Debug.Log("Rotating door towards target rotation.");
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Stop wanneer bereikt
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
                isOpening = false;
        }
    }

}
