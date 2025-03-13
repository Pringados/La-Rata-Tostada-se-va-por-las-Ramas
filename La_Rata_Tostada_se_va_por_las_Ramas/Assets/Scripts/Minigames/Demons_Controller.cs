using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Demons_Controller : IMinigame
{
    [SerializeField] private float resetTime;

    [SerializeField] private int maxCounter;

    [SerializeField] private Text message;

    [SerializeField] private int points;

    private int counter; 

    private bool reset;

    private void OnEnable()
    {
        message.text = string.Empty; 

        counter = 0; 
    }

    public void DemonClick()
    {
        if (reset) return; 

        counter++; 

        if (maxCounter <= counter)
        {
            message.text = "VICTORIA";

            MinigameComplete(true);

            StartCoroutine(ResetDemons()); 
        }
    }

    private IEnumerator ResetDemons()
    {
        reset = true;

        yield return new WaitForSeconds(resetTime);

        message.text = string.Empty;

        counter = 0; 

        reset = false;
    }

    public override int CalculateScore()
    {
        return points;
    }
}
