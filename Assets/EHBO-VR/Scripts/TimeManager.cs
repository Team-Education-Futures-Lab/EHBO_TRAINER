using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float TimeInMinutes = 3;
    public bool WithText = true;

    public TextMeshProUGUI Timer;
    public Slider timerSlider;

    private float TimeInSeconds;
    private bool stopTimer;

    void Start()
    {
        TimeInSeconds = TimeInMinutes * 60f;
        stopTimer = false;

        timerSlider.maxValue = TimeInSeconds;
        timerSlider.value = TimeInSeconds;

        if (WithText)
            Timer.gameObject.SetActive(true);
        else
            Timer.gameObject.SetActive(false);

        Timer.text = $"{TimeInMinutes:0}:00";
    }

    void Update()
    {
        if (stopTimer)
            return;

        TimeInSeconds -= Time.deltaTime;

        if (TimeInSeconds <= 0)
        {
            TimeInSeconds = 0;
            stopTimer = true;
        }

        int minutes = Mathf.FloorToInt(TimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(TimeInSeconds % 60);

        Timer.text = $"{minutes:0}:{seconds:00}";
        timerSlider.value = TimeInSeconds;
    }
}