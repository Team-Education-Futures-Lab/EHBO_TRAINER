using System.Collections;
using UnityEngine;

public class PlayerTransitionToTutorial : MonoBehaviour
{
    [SerializeField]
    private bool isInTransition = false;

    [Header("VR Camera Rig")]
    [SerializeField]
    private Transform cameraRig;

    [Header("Target Positions")]
    [SerializeField]
    private Transform gameTutorialTarget;
    [SerializeField]
    private Transform vrTutorialTarget;

    [Header("Fade")]
    [SerializeField]
    private SceneFade sceneFade;
    [SerializeField]
    private float fadeDuration = 1f;

    [Header("Audio")]
    [SerializeField]
    private AudioSource transitionAudio; // Audio source used during the transition

    void Start()
    {
        if (transitionAudio != null)
            transitionAudio.loop = true; // zorg dat het blijft loopen
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

        // Start audio bij begin van transition
        if (transitionAudio != null && !transitionAudio.isPlaying)
            transitionAudio.Play();

        // Fade out
        if (sceneFade != null)
            yield return StartCoroutine(sceneFade.FadeOutCoroutine(fadeDuration));

        // Camera verplaatsen
        cameraRig.position = target.position;
        cameraRig.rotation = target.rotation;

        // EventSystem reset (belangrijk voor XR)
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        // Fade in
        if (sceneFade != null)
            yield return StartCoroutine(sceneFade.FadeInCoroutine(fadeDuration));

        // Stop audio aan einde van transition
        if (transitionAudio != null && transitionAudio.isPlaying)
            transitionAudio.Stop();

        isInTransition = false;
    }
}
