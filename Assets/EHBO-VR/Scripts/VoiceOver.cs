using UnityEngine;

public class VoiceOver : MonoBehaviour
{
    [SerializeField] private AudioSource voiceOverSource;

    public void PlayVoiceOver(AudioClip clip)
    {
        if (clip == null || voiceOverSource == null)
            return;

        voiceOverSource.Stop();
        voiceOverSource.clip = clip;
        voiceOverSource.Play();
    }
    public void StopVoiceOver()
    {
        if (voiceOverSource != null && voiceOverSource.isPlaying)
            voiceOverSource.Stop();
    }
}
