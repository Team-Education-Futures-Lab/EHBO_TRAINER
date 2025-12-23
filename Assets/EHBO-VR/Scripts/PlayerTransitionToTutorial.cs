using System.Collections;
using UnityEngine;

public class PlayerTransitionToTutorial : MonoBehaviour
{
   
    public bool isInTransition = false;

    [Header("VR Camera Rig")]
    public Transform cameraRig;        // Bijv. OVR Camera Rig / XR Origin
    public Transform targetPosition;   // Lege GameObject met gewenste positie/rotatie

    [Header("NPC Object")]
    public GameObject npcObject;
    public MonoBehaviour scriptToActivate; // script dat de NPC laat lopen
    public Animator animator; // Animator van de NPC

    [Header("Fade")]
    public SceneFade sceneFade; // Sleep hier je fade object in de Inspector
    public float fadeDuration = 1f;


    public void MoveCameraRig()
    {
        if (cameraRig == null || targetPosition == null)
        {
            Debug.LogWarning("CameraRig of TargetPosition is niet ingesteld.");
            return;
        }

        cameraRig.position = targetPosition.position;
        cameraRig.rotation = targetPosition.rotation;

        Debug.Log("Camera rig verplaatst.");
    }
    void Start()
    {
        npcObject.SetActive(false);
    }

    public void StartTransition()
    {
        StartCoroutine(TransitionRoutine());
    }

    IEnumerator TransitionRoutine()
    {
        // Fade out
        if (sceneFade != null)
            yield return StartCoroutine(sceneFade.FadeOutCoroutine(fadeDuration));

        // Verplaats de VR camera rig
        MoveCameraRig();

        // NPC transition
        NPCTransition();

        // EventSystem reset zodat VR-knoppen weer werken
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        // Fade in
        if (sceneFade != null)
            yield return StartCoroutine(sceneFade.FadeInCoroutine(fadeDuration));
    }



    private void NPCTransition()
    {
    
        // Script meteen activeren
        if (scriptToActivate != null)
            scriptToActivate.enabled = true;

        // Optioneel: animatie direct starten
        if (animator != null)
            animator.SetTrigger("Walk"); // alleen als je een trigger gebruikt

        isInTransition = true;
        npcObject.SetActive(true);
    }
}
