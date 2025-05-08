using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Popups : IMinigame
{
    [SerializeField] private GameObject window;

    [SerializeField] private float resetTime;

    [SerializeField] private int points;
    [SerializeField] private int minWindows;
    [SerializeField] private int maxWindows;

    [SerializeField] Text popupTextComponent;
    [SerializeField] string[] windowMessages;

    private bool reset = false;

    private int counter;
    private int maxCounter;

    private void OnEnable()
    {
        counter = 0;

        maxCounter = Random.Range(minWindows, maxWindows);

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

            MinigameComplete(true);

            StartCoroutine(ResetWindow());
        }
    }

    private void ChangeWindow()
    {
        if (window.GetComponent<RectTransform>() != null)
        {
            float w = Random.Range(300, 600);
            float h = Random.Range(300, 500);

            window.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);

            window.GetComponent<RectTransform>().position =
                new Vector2(Random.Range(w / 2, Screen.width - w / 2), Random.Range(h, Screen.height * 0.8f - h));
            Debug.Log("Screen width: " + Screen.width + "    height: " + Screen.height);

            popupTextComponent.text = windowMessages[Random.Range(0, windowMessages.Length)].Replace("NEWLINE", "\n");
        }
    }

    private IEnumerator ResetWindow()
    {
        reset = true;

        yield return new WaitForSeconds(resetTime);

        maxCounter = Random.Range(1, maxWindows + 1);

        reset = false;
    }

    public override float CalculateScore()
    {
        return points;
    }
}
