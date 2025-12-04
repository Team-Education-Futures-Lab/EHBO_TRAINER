using UnityEngine;
using System.Collections;

public class PlayerTransitionToTutorial : MonoBehaviour
{
    public float blinkInterval = 0.2f;
    public int blinkCount = 6;
    public MonoBehaviour scriptToActivate; // script dat je wilt activeren

    private SkinnedMeshRenderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // Begin onzichtbaar
        foreach (var rend in renderers)
            rend.enabled = false;

        // Zet het script uit totdat nodig
        if (scriptToActivate != null)
            scriptToActivate.enabled = false;
        ActivatePlayer();
    }

    // Deze functie kan je koppelen aan een VR-knop
    public void ActivatePlayer()
    {
        StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        bool visible = true;

        for (int i = 0; i < blinkCount; i++)
        {
            visible = !visible;
            foreach (var rend in renderers)
                rend.enabled = visible;

            yield return new WaitForSeconds(blinkInterval);
        }

        // Eindig zichtbaar
        foreach (var rend in renderers)
            rend.enabled = true;

        // Activeer het andere script
        if (scriptToActivate != null)
            scriptToActivate.enabled = true;
    }
}
