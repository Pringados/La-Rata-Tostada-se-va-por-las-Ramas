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

    [SerializeField] private Text text;

    [SerializeField] private Image nextButton;

    [SerializeField] private GameObject endButton;

    private bool reset = false; 

    private int counter;
    private int maxCounter;
    private bool terminado = false;

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

            if (maxCounter <= counter && !terminado)
            {
                nextButton.enabled = true;
                text.text = "NO ME HE ENTERADO, REPITELO POR FAVOR";
                terminado = true;
                StartCoroutine(ButtonSpawn());
                //MinigameComplete(true); 
            }
            else if (terminado)
            {
                text.text = "";
                nextButton.enabled = false;
                endButton.SetActive(false);
                terminado = false;
                ModifyPowerPoint();
            }
            else
                window.sprite = slides[powerpoint[counter]];
        }

        else
        {
            StartCoroutine(ResetPowerPoint());

            MinigameComplete(true);
        }
    }

    private void ModifyPowerPoint()
    {
        counter = 0; 

        maxCounter = Random.Range(3, maxWindows + 1);

        powerpoint = new int[maxCounter];

        for (int i = 0; i < maxCounter; i++)
            powerpoint[i] = Random.Range(0, slides.Length);

        window.sprite = slides[powerpoint[counter]];
    }

    private IEnumerator ButtonSpawn()
    {

        yield return new WaitForSeconds(resetTime);

        endButton.SetActive(true);
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
