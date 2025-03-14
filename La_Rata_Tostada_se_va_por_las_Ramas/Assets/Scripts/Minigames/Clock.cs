using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using System.Collections.Generic;

public class Clock : IMinigame
{
    [SerializeField] private Transform minuteClockhand; 
    [SerializeField] private Transform hourClockhand;

    [SerializeField] private Text message;

    [SerializeField] private float resetTime;

    [SerializeField] private int points;

    private bool reset;

    private float auxhourAngle;
    private float hourAngle;
    private int minuteAngle; 

    private void OnEnable()
    {
        ResetHour(); 
    }

    private void OnMouseDown()
    {
        if (reset) return; 

        minuteClockhand.Rotate(Vector3.back, 30);

        hourClockhand.Rotate(Vector3.back, 2.5f);

        if ((Mathf.Round(minuteClockhand.rotation.eulerAngles.z * 2) / 2) == minuteAngle &&
            ((Mathf.Round(hourClockhand.rotation.eulerAngles.z * 2) / 2) <= hourAngle &&
            (Mathf.Round(hourClockhand.rotation.eulerAngles.z * 2) / 2) > auxhourAngle))
        {
            MinigameComplete(true);

            StartCoroutine(ResetClock()); 
        }
    }

    private void ResetHour()
    {
        minuteClockhand.rotation = Quaternion.Euler(0f, 0f, 0f);
        hourClockhand.rotation = Quaternion.Euler(0f, 0f, 0f);

        int hour = (int)Random.Range(1, 12);

        hourAngle = 360 - (30 * hour);
        auxhourAngle = 360 - (30 * (hour + 1)); 

        int minute = (int)Random.Range(1, 12);

        minuteAngle = 360 - (30 * minute);

        if (minute * 5 == 5)
            message.text = hour + ":0" + (minute * 5);
        else
            message.text = hour + ":" + (minute * 5);
    }

    private IEnumerator ResetClock()
    {
        reset = true;

        message.text = "BINGO"; 

        yield return new WaitForSeconds(resetTime);

        ResetHour();

        reset = false;
    }

    public override int CalculateScore()
    {
        return points;
    }
}
