using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public Animator animator;
    public AudioSource engineAudio;

    void Update()
    {
        if (!animator.enabled) // check of animatie uit staat
        {
            if (engineAudio.isPlaying)
                engineAudio.Stop();
            return; // niks anders doen
        }

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

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
