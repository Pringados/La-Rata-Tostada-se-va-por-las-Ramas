using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Captcha : IMinigame
{
    [SerializeField] private Sprite[] correct;
    [SerializeField] private Sprite[] incorrect;

    [SerializeField] private Image[] buttons;

    [SerializeField] private Text message;

    [SerializeField] private float resetTime;

    [SerializeField] private int points;

    private bool[] answers;

    private bool reset; 

    private int counter;
    private int right; 

    private void OnEnable()
    {
        ResetCaptcha(); 
    }

    private void ResetCaptcha()
    {
        counter = 0;
        right = 0;

        int wrong = 0;

        message.text = string.Empty;

        answers = new bool[buttons.Length]; 

        for (int i = 0; i < buttons.Length; i++)
        {
            int n = Random.Range(1, 3);

            if (n % 2 == 0)
            {
                if (right < correct.Length)
                {
                    buttons[i].sprite = correct[right];

                    answers[i] = true; 

                    right++;
                }

                else if (wrong < incorrect.Length)
                {
                    buttons[i].sprite = incorrect[wrong];

                    answers[i] = false;

                    wrong++;
                }

                else
                    answers[i] = false;
            }

            else
            {
                if (wrong < incorrect.Length)
                {
                    buttons[i].sprite = incorrect[wrong];

                    answers[i] = false;

                    wrong++;
                }

                else if (right < correct.Length)
                {
                    buttons[i].sprite = correct[right];

                    answers[i] = true;

                    right++;
                }

                else
                    answers[i] = false;
            }
        }
    }

    public void OnButtonSelect(int value)
    {
        if (reset) return;

        if (answers[value]) counter++; 
    }
    public void OnAcceptSelect()
    {
        if (reset) return; 

        if (counter == right)
        {
            message.text = "CORRECT";
            MinigameComplete(true);
        }

        else
            message.text = "WRONG";

        StartCoroutine(ResetCaptchaCode());
    }
    private IEnumerator ResetCaptchaCode()
    {
        reset = true;

        yield return new WaitForSeconds(resetTime);

        ResetCaptcha();

        reset = false;
    }

    public override int CalculateScore()
    {
        return points;
    }
}
