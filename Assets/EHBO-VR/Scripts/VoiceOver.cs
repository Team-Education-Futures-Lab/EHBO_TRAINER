using UnityEngine;
using System.Collections;

public class VoiceOver : MonoBehaviour
{
    [SerializeField] private AudioSource voiceOverSource;
    [SerializeField] private AudioClip IntroTutorialsClip;       // de voice over audio van de intro/menu
    [SerializeField] private float delayIntroVoiceOver = 2f;
    void Start()
    {
        if (IntroTutorialsClip != null)
        {
            StartCoroutine(PlayVoiceOverWithDelay(IntroTutorialsClip, delayIntroVoiceOver));
        }
    }

    private IEnumerator PlayVoiceOverWithDelay(AudioClip clip, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PlayVoiceOver(clip); // gewoon dit script zelf gebruiken
    }

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
