using System.Collections;
using UnityEngine;

public class PlayerTransitionToTutorial : MonoBehaviour
{
    public bool isInTransition = false;

    [Header("VR Camera Rig")]
    public Transform cameraRig;

    [Header("Target Positions")]
    public Transform gameTutorialTarget;
    public Transform vrTutorialTarget;

    [Header("NPC Object")]
    public GameObject npcObject;
    public MonoBehaviour scriptToActivate;
    public Animator animator;

    [Header("Fade")]
    public SceneFade sceneFade;
    public float fadeDuration = 1f;

    void Start()
    {
        if (npcObject != null)
            npcObject.SetActive(false);
    }

    // 🔹 Deze koppel je aan de GAME tutorial knop
    public void StartGameTutorial()
    {
        if (isInTransition) return;
        StartCoroutine(TransitionRoutine(gameTutorialTarget));
    }

    // 🔹 Deze koppel je aan de VR tutorial knop
    public void StartVRTutorial()
    {
        if (isInTransition) return;
        StartCoroutine(TransitionRoutine(vrTutorialTarget));
    }

    IEnumerator TransitionRoutine(Transform target)
    {
        if (cameraRig == null || target == null)
        {
            Debug.LogWarning("CameraRig of TargetPosition is niet ingesteld.");
            yield break;
        }

        isInTransition = true;

        // Fade out
        if (sceneFade != null)
            yield return StartCoroutine(sceneFade.FadeOutCoroutine(fadeDuration));

        // Camera verplaatsen
        cameraRig.position = target.position;
        cameraRig.rotation = target.rotation;

        // NPC transition
        //NPCTransition();

        // EventSystem reset (belangrijk voor XR)
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        // Fade in
        if (sceneFade != null)
            yield return StartCoroutine(sceneFade.FadeInCoroutine(fadeDuration));
    }

    void NPCTransition()
    {
        if (scriptToActivate != null)
            scriptToActivate.enabled = true;

        if (animator != null)
            animator.SetTrigger("Walk");

        if (npcObject != null)
            npcObject.SetActive(true);
    }
}
