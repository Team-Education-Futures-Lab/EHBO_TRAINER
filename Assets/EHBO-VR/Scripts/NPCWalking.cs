using UnityEngine;
using System.Collections;


public class NPCWalking : MonoBehaviour
{
    public enum MoveState { Idle, Walk, Run }

    public Transform[] waypoints;
    public MoveState currentState = MoveState.Walk; // ← kies in Inspector
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float waitTime = 2f;
    public Animator animator;

    private int currentWaypoint = 0;
    private bool isWaiting = false;

    void Update()
    {
        if (waypoints.Length == 0 || isWaiting) return;

        Vector3 target = waypoints[currentWaypoint].position;
        float distance = Vector3.Distance(transform.position, target);
        Vector3 direction = (target - transform.position).normalized;

        if (distance > 0.1f)
        {
            // Kies snelheid op basis van MoveState
            float moveSpeed = 0f;
            switch (currentState)
            {
                case MoveState.Idle:
                    moveSpeed = 0f;
                    break;
                case MoveState.Walk:
                    moveSpeed = walkSpeed;
                    break;
                case MoveState.Run:
                    moveSpeed = runSpeed;
                    break;
            }

            // Beweeg en roteer NPC
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);

            // ✅ Normaliseer snelheid voor de Blend Tree (0 = idle, 0.5 = walk, 1 = run)
            float blendValue = 0f;
            if (currentState == MoveState.Idle)
                blendValue = 0f;
            else if (currentState == MoveState.Walk)
                blendValue = 1f;
            else if (currentState == MoveState.Run)
                blendValue = 3f;

            animator.SetFloat("Speed", blendValue);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        isWaiting = false;
    }
}

