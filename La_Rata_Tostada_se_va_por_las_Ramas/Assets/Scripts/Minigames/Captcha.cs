using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Captcha : IMinigame
{
    [SerializeField] private Sprite[] loki;
    [SerializeField] private Sprite[] thor;
    [SerializeField] private Sprite[] odin;
    [SerializeField] private Sprite[] extras;

    [SerializeField] private GameObject[] buttons;

    [SerializeField] private Text message;

    [SerializeField] private float resetTime;

    [SerializeField] private int points;

    private Sprite[] correct;
    private Sprite[] incorrect;

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

        int img = Random.Range(0, 4); 

        if (img == 0)
        {
            correct = new Sprite[odin.Length];
            correct = odin;

            message.text = "Padre de todos, él reina en Asgard";

            CreateGroups(thor, loki);
        }

        else if (img == 1)
        {
            correct = new Sprite[loki.Length];
            correct = loki;

            message.text = "No te vuelvas Loki";

            CreateGroups(odin, thor); 
        }

        else
        {
            correct = new Sprite[thor.Length];
            correct = thor;

            message.text = "Porque yo. soy. THOR";

            CreateGroups(loki, odin);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int n = Random.Range(1, 3);

            buttons[i].GetComponent<Button>().interactable = true; 

            if (n % 2 == 0)
            {
                if (right < correct.Length)
                {
                    buttons[i].GetComponent<Image>().sprite = correct[right];

                    answers[i] = true; 

                    right++;
                }

                else if (wrong < incorrect.Length)
                {
                    buttons[i].GetComponent<Image>().sprite = incorrect[wrong];

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
                    buttons[i].GetComponent<Image>().sprite = incorrect[wrong];

                    answers[i] = false;

                    wrong++;
                }

                else if (right < correct.Length)
                {
                    buttons[i].GetComponent<Image>().sprite = correct[right];

                    answers[i] = true;

                    right++;
                }

                else
                    answers[i] = false;
            }
        }
    }

    private void CreateGroups(Sprite[] first, Sprite[] second)
    {
        incorrect = new Sprite[first.Length + second.Length + extras.Length];

        int f = 0;
        int s = 0;
        int aux = 0; 

        for (int i = 0; i < first.Length + second.Length + extras.Length; i++)
        {
            int n = Random.Range(0, 3);

            if (n == 0)
            {
                if (f < first.Length)
                {
                    incorrect[i] = first[f];

                    f++;
                }

                else if (aux < extras.Length)
                {
                    incorrect[i] = extras[aux];

                    aux++;
                }

                else if (s < second.Length)
                {
                    incorrect[i] = second[s];

                    s++;
                }

            }

            else if (n == 1)
            {
                if (s < second.Length)
                {
                    incorrect[i] = second[s];

                    s++;
                }

                else if (f < first.Length)
                {
                    incorrect[i] = first[f];

                    f++;
                }

                else
                {
                    incorrect[i] = extras[aux];

                    aux++;
                }
            }

            else
            {
                if (aux < extras.Length)
                {
                    incorrect[i] = extras[aux];

                    aux++;   
                }

                else if (f < first.Length)
                {
                    incorrect[i] = first[f];

                    f++;
                }

                else
                {
                    incorrect[i] = second[s];

                    s++;
                }
            }
        }
    }

    public void OnButtonSelect(int value)
    {
        if (reset) return;

        if (answers[value]) counter++;

        buttons[value].GetComponent<Button>().interactable = false; 
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

    public override float CalculateScore()
    {
        return points;
    }
}
