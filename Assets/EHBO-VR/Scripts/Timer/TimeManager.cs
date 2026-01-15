using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private float TimeInMinutes = 3;
    [SerializeField]
    private bool WithText = true;

    [SerializeField]
    private TextMeshProUGUI Timer;
    [SerializeField]
    private Slider timerSlider;

    private float TimeInSeconds;
    private bool stopTimer;

    [SerializeField]
    private AudioSource TickingSound;

    void Update()
    {
        if (stopTimer)
            return;

        TimeInSeconds -= Time.deltaTime;

        if (TimeInSeconds <= 0)
        {
            TimeInSeconds = 0;
            stopTimer = true;
            TickingSound.Stop();
        }
        else
        {
            if (!TickingSound.isPlaying)
                TickingSound.Play();
        }

        int minutes = Mathf.FloorToInt(TimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(TimeInSeconds % 60);

        Timer.text = $"{minutes:0}:{seconds:00}";
        timerSlider.value = TimeInSeconds;
    }
    public void ResetTimer(float timeInSeconds)
    {
        TimeInSeconds = timeInSeconds;
        timerSlider.maxValue = TimeInSeconds;
        timerSlider.value = TimeInSeconds;

        int minutes = Mathf.FloorToInt(TimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(TimeInSeconds % 60);
        Timer.text = $"{minutes:0}:{seconds:00}";

        stopTimer = false;
    }

}
