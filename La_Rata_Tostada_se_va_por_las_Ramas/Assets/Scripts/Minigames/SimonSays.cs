using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SimonSays : IMinigame
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private int[] lightOrder;
    [SerializeField] private int points;
    [SerializeField] private float resetTime;
    [SerializeField] private GameObject[] notes;
    [SerializeField] private GameObject notas;
    public List<GameObject> notesSpawn;
    int buttonsClicked = 0;
    int colorOrderCount = 4;
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
        Debug.Log(button);
        switch (buttonsClicked)
        {
            case 1:
                notesSpawn.Add(Instantiate(notes[button], notas.transform));
                notesSpawn[0].transform.localPosition = new Vector3(-219, -82, 0);
                notesSpawn[0].SetActive(true);
                break;
            case 2:
                notesSpawn.Add(Instantiate(notes[button], notas.transform));
                notesSpawn[1].transform.localPosition = new Vector3(-145, -31, 0);
                notesSpawn[1].SetActive(true);
                break;
            case 3:
                notesSpawn.Add(Instantiate(notes[button], notas.transform));
                notesSpawn[2].transform.localPosition = new Vector3(-193, 12, 0);
                notesSpawn[2].SetActive(true);
                break;
            case 4:
                notesSpawn.Add(Instantiate(notes[button], notas.transform));
                notesSpawn[3].transform.localPosition = new Vector3(-29, 89, 0);
                notesSpawn[3].SetActive(true);
                break;
        }
        if (button == lightOrder[buttonsClicked - 1])
        {
            next = true;
            Debug.Log("vAS BIEN");
        }
        else
        {
            won = false;
            next = false;
            buttonsClicked = 0;
            Debug.Log("MAL");
            StartCoroutine(ResetSimonSaysCode());
        }
        if (buttonsClicked == colorOrderCount && next)
        {
            won = true;
            next = false;
            StartCoroutine(ResetSimonSaysCode());
            MinigameComplete(true);
        }
    }

    private IEnumerator ColorOrder()
    {
        //Aqui va el codigo para el spawneo de colores
        buttonsClicked = 0;
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
        while(notesSpawn.Count > 0)
        {
            Destroy(notesSpawn[0]);
            notesSpawn.RemoveAt(0);
        }
        for (int i = 0; i < lightOrder.Length; i++)
        {
            lightOrder[i] = Random.Range(0, 4);
        }

        StartCoroutine(ColorOrder());
    }

    private IEnumerator ResetSimonSaysCode()
    {
        reset = true;

        DisableButtons();

        yield return new WaitForSeconds(resetTime);

        ResetSimonSays();

        reset = false;
    }

    public override float CalculateScore()
    {
        return points;
    }
}
