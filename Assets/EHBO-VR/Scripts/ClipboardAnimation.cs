using UnityEngine;
using UnityEngine.XR;

public class ClipboardAnimation : MonoBehaviour
{
    public Animator animator;    // Sleep hier je Animator component in
    public string animationName; // Naam van de animatieclip in de Animator
    public float pauseTime = 7f; // Tijd waarop animatie pauzeert

    private bool paused = false;
    private bool hasResumed = false;

    void Start()
    {
        animator.Play(animationName);
    }

    void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // Animatie pauzeren na pauseTime
        if (!paused && state.normalizedTime * state.length >= pauseTime)
        {
            animator.speed = 0f;
            paused = true;
        }

        // Alleen checken of we al hervat hebben
        if (paused && !hasResumed)
        {
            // Check input van controllers
            if (ControllerMoved())
            {
                animator.speed = 1f; // Hervat animatie
                hasResumed = true;   // Niet meer blijven checken
            }
        }
    }

    bool ControllerMoved()
    {
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        Vector2 leftAxis, rightAxis;
        bool leftMoved = leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftAxis) && leftAxis.magnitude > 0.1f;
        bool rightMoved = rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightAxis) && rightAxis.magnitude > 0.1f;

        return leftMoved || rightMoved;
    }
}
