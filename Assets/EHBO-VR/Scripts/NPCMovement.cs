using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour
{
    public enum MoveState { Idle, Walk, Run }

    [Header("Movement Settings")]
    public Transform[] waypoints;
    public MoveState currentState = MoveState.Walk;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float waitTime = 0f;
    public bool loopPath = false;

    [Header("References")]
    public Animator animator;
    public Transform lookAtTarget;

    private int currentWaypoint = 0;
    private bool isWaiting = false;

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Only move when he is not waiting
        if (!isWaiting)
        {
            MoveTowardsWaypoint();
        }

        //If loop is not on and he is at the last waypoint look at the target
        if (!loopPath && currentWaypoint == waypoints.Length - 1 && lookAtTarget != null)
        {
            LookAtTarget();
        }
    }

    // ===============================
    // 🔹 Movement Logic
    // ===============================
    private void MoveTowardsWaypoint()
    {
        Vector3 target = waypoints[currentWaypoint].position;
        float distance = Vector3.Distance(transform.position, target);
        Vector3 direction = (target - transform.position).normalized;

        if (distance > 0.1f)
        {
            float moveSpeed = GetMoveSpeed();
            MoveNPC(direction, moveSpeed);
            RotateTowards(direction);
            UpdateAnimationSpeed();
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            StartCoroutine(WaitBeforeNextWaypoint());
        }
    }

    private float GetMoveSpeed()
    {
        switch (currentState)
        {
            case MoveState.Idle: return 0f;
            case MoveState.Walk: return walkSpeed;
            case MoveState.Run: return runSpeed;
            default: return 0f;
        }
    }

    private void MoveNPC(Vector3 direction, float speed)
    {
        // move NPC
        transform.position += direction * speed * Time.deltaTime;
    }

    private void RotateTowards(Vector3 direction)
    {
        // Smooth rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
    }

    private void UpdateAnimationSpeed()
    {
        // How fast the animation is
        float blendValue = 0f;
        if (currentState == MoveState.Idle)
            blendValue = 0f;
        else if (currentState == MoveState.Walk)
            blendValue = 1f;
        else if (currentState == MoveState.Run)
            blendValue = 3f;

        animator.SetFloat("Speed", blendValue);
    }

    // ===============================
    // 🔹 Waypoint Handling
    // ===============================
    private IEnumerator WaitBeforeNextWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        HandleWaypointProgression();
    }

    private void HandleWaypointProgression()
    {
        // if loopPath is true walk back to the first waypoint
        if (loopPath)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            isWaiting = false;
        }
        else
        {
            // else stop at the last waypoint
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
                isWaiting = false;
            }
            else
            {
                StopAtLastWaypoint();
            }
        }
    }

    private void StopAtLastWaypoint()
    {
        currentState = MoveState.Idle;
        animator.SetFloat("Speed", 0f);
        //lookAtTarget happends now in update()
    }

    // ===============================
    // 🔹 Look at Target (at last waypoint)
    // ===============================
    private void LookAtTarget()
    {
        Vector3 lookDirection = (lookAtTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }
}
