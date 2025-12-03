using UnityEngine;
using System.Collections;

public class NPCBlink : MonoBehaviour
{
    public float blinkInterval = 0.2f; // tijd tussen knipperen
    public int blinkCount = 6; // aantal keer knipperen (aan/uit telt elk als 1)

    private SkinnedMeshRenderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        bool visible = true;

        for (int i = 0; i < blinkCount; i++)
        {
            visible = !visible;
            foreach (var rend in renderers)
                rend.enabled = visible;

            yield return new WaitForSeconds(blinkInterval);
        }

        // Zorg dat NPC zichtbaar eindigt
        foreach (var rend in renderers)
            rend.enabled = true;
    }
}
