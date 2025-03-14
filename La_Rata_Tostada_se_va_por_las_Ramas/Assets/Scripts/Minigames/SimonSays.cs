using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimonSays : IMinigame
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private int[] lightOrder;
    [SerializeField] private int points;
    [SerializeField] private float resetTime;
    [SerializeField] private GameObject[] notes;

    int buttonsClicked = 0;
    int colorOrderCount = 3;
    bool next = false;
    bool won = false;
    bool reset = false;
    public float speed = 0.5f;

    private void OnEnable()
    {
        ResetSimonSays();
    }

    public void OnButtonSelect(int button)
    {
        buttonsClicked++;
        if (button == lightOrder[buttonsClicked - 1])
        {
            next = true;
        }
        else
        {
            won = false;
            next = false;
            buttonsClicked = 0;
            StartCoroutine(ResetSimonSaysCode());
        }
        if (buttonsClicked == colorOrderCount && next)
        {
            won = true;
            next = false;
            MinigameComplete(true);
            StartCoroutine(ResetSimonSaysCode());
        }
    }

    private IEnumerator ColorOrder()
    {
        //Aqui va el codigo para el spawneo de colores
        buttonsClicked = 0;
        colorOrderCount++;
        DisableButtons();
        for (int i = 0; i < colorOrderCount; i++)
        {
            yield return new WaitForSeconds(speed);
            notes[lightOrder[i]].SetActive(true);
            yield return new WaitForSeconds(speed);
            notes[lightOrder[i]].SetActive(false);
        }
        EnableButtons();
    }

    private void DisableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    private void EnableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    private void ResetSimonSays()
    {
        for (int i = 0; i < lightOrder.Length; i++)
        {
            lightOrder[i] = Random.Range(0, 4);
        }

        StartCoroutine(ColorOrder());
    }

    private IEnumerator ResetSimonSaysCode()
    {
        reset = true;

        yield return new WaitForSeconds(resetTime);

        ResetSimonSays();

        reset = false;
    }

    public override int CalculateScore()
    {
        return points;
    }
}
