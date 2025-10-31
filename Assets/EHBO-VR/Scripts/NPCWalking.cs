using UnityEngine;
using System.Collections;


public class NPCWalking : MonoBehaviour
{
    public enum MoveState { Idle, Walk, Run }

    public Transform[] waypoints;
    public MoveState currentState = MoveState.Walk;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float waitTime = 0f;
    public Animator animator;
    public bool loopPath = false;

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
   
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);

            //How fast the animation is
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

        //if loopPatch is true walk back to the first waypoint
        if (loopPath)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            isWaiting = false;
        }
        else
        {
            //else stop at the last waypoint
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
                isWaiting = false;
            }
            else
            {
                currentState = MoveState.Idle;
                animator.SetFloat("Speed", 0f);
            }
        }
    }
}

