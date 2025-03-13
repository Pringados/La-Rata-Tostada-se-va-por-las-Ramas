using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Popups : IMinigame
{
    [SerializeField] private GameObject window;

    [SerializeField] private Text message; 

    [SerializeField] private float resetTime;

    [SerializeField] private int points;
    [SerializeField] private int maxWindows;

    private bool reset = false;

    private int counter;
    private int maxCounter;

    private void OnEnable()
    {
        counter = 0;

        message.text = "HOLA";

        maxCounter = Random.Range(1, maxWindows + 1);

        ChangeWindow(); 
    }

    public void CloseWindow()
    {
        if (reset) return; 

        if (counter < maxCounter)
        {
            counter++;

            ChangeWindow(); 
        }

        else
        {
            counter = 0; 

            message.text = "CORRECT";

            MinigameComplete(true);

            StartCoroutine(ResetWindow());
        }
    }

    private void ChangeWindow()
    {
        if (window.GetComponent<RectTransform>() != null)
        {
            float w = Random.Range(100, 500);
            float h = Random.Range(100, 500);

            window.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);

            window.GetComponent<RectTransform>().position =
                new Vector2(Random.Range(0, Screen.width - w), Random.Range(0, Screen.height - h));
        }
    }

    private IEnumerator ResetWindow()
    {
        reset = true;

        yield return new WaitForSeconds(resetTime);

        maxCounter = Random.Range(1, maxWindows + 1);

        message.text = "HOLA";

        reset = false;
    }

    public override int CalculateScore()
    {
        return points;
    }
}
