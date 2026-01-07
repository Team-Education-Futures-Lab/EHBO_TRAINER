using UnityEngine;

public class CarEngineAuto : MonoBehaviour
{
    public Animator animator;
    public AudioSource engineAudio;

    void Update()
    {
        // Krijg info over de huidige animatie op layer 0
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // Als de animatie nog niet klaar is (normalizedTime < 1) of looped
        if (state.normalizedTime < 1f || state.loop)
        {
            if (!engineAudio.isPlaying)
                engineAudio.Play();
        }
        else
        {
            if (engineAudio.isPlaying)
                engineAudio.Stop();
        }
    }
}
