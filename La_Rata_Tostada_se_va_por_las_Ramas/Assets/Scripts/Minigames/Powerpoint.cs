using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Powerpoint : IMinigame
{
    [SerializeField] private int points;

    [SerializeField] private int maxWindows;

    [SerializeField] private float resetTime; 

    [SerializeField] private Sprite[] slides;

    [SerializeField] private Image window;

    private bool reset = false; 

    private int counter;
    private int maxCounter;

    private int[] powerpoint; 

    private void OnEnable()
    {
        ModifyPowerPoint(); 
    }

    public void OnButtonClick(bool next)
    {
        if (reset) return; 

        if (next)
        {
            counter++;

            if (maxCounter <= counter)
            {
                StartCoroutine(ResetPowerPoint());

                MinigameComplete(true); 
            }

            else
                window.sprite = slides[powerpoint[counter]];
        }

        else
        {
            counter--;

            if (counter < 0) counter = 0;

            window.sprite = slides[powerpoint[counter]];
        }
    }

    private void ModifyPowerPoint()
    {
        counter = 0; 

        maxCounter = Random.Range(1, maxWindows + 1);

        powerpoint = new int[maxCounter];

        for (int i = 0; i < maxCounter; i++)
            powerpoint[i] = Random.Range(0, slides.Length);

        window.sprite = slides[powerpoint[counter]];
    }

    private IEnumerator ResetPowerPoint()
    {
        reset = true;

        yield return new WaitForSeconds(resetTime);

        ModifyPowerPoint(); 

        reset = false;
    }

    public override int CalculateScore()
    {
        return points;
    }
}
