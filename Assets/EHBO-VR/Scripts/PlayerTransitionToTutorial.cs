using UnityEngine;
using System.Collections;

public class PlayerTransitionToTutorial : MonoBehaviour
{
    public float blinkInterval = 0.2f;
    public int blinkCount = 6;
    public MonoBehaviour scriptToActivate; // script dat je wilt activeren
    public Animator animator; // Animator van de speler
    public bool isInTransition = false; // standaard false


    private SkinnedMeshRenderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var rend in renderers)
            rend.enabled = false;

        if (scriptToActivate != null)
            scriptToActivate.enabled = false;

        ActivatePlayer();
    }

    public void ActivatePlayer()
    {
        StartCoroutine(BlinkEffect());
        isInTransition = true;

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

        foreach (var rend in renderers)
            rend.enabled = true;

        if (scriptToActivate != null)
            scriptToActivate.enabled = true;

    }
}
