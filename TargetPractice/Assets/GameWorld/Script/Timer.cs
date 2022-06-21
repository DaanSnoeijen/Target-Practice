using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text TimerText;

    public static Timer Instance;

    Coroutine routine;

    int timer;
    string time;

    private void Awake()
    {
        //Create a singleton pattern that doesn't get destroyed on load
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTimer()
    {
        TimerText.text = "0:00";

        routine = StartCoroutine(IStartTimer());
    }

    public string StopTimer()
    {
        TimerText.text = "";
        StopCoroutine(routine);

        return time;
    }

    IEnumerator IStartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            timer++;

            int minutes = timer / 60;
            int seconds = timer - (60 * minutes);
            time = minutes + ":";
            if (seconds < 10) time += "0";
            time += seconds;

            TimerText.text = time;

            if (timer == 60) Night.Instance.StartNight();
        }
    }
}
