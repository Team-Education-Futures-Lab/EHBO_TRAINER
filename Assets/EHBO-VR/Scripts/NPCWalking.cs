using UnityEngine;
using System.Collections;

public class NPCWalking : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 3f;
    public Animator animator;
    public float waitTime = 2f; // tijd in seconden dat hij wacht bij een waypoint

    private int currentWaypoint = 0;
    private bool isWaiting = false;

    void Update()
    {
        if (waypoints.Length == 0 || isWaiting) return;

        Vector3 target = waypoints[currentWaypoint].position;
        Vector3 direction = (target - transform.position).normalized;

        float distance = Vector3.Distance(transform.position, target);

        if (distance > 0.1f)
        {
            // NPC beweegt
            Vector3 move = direction * moveSpeed * Time.deltaTime;
            transform.position += move;
            transform.rotation = Quaternion.LookRotation(direction);

            // Walking animatie
            animator.SetFloat("Speed", moveSpeed);
        }
        else
        {
            // Stop en start Idle animatie
            animator.SetFloat("Speed", 0f);

            // Start wacht-couroutine
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // Ga naar volgende waypoint
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        isWaiting = false;
    }
}
