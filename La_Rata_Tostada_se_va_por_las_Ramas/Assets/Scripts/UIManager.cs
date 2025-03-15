using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

//enum messageColors
//{
//    red, green, blue
//}
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance;

    [SerializeField]
    List<GameObject> letters = new List<GameObject>();
    private List<bool> busyLetters = new List<bool>();

    [SerializeField]
    List<Sprite> estadosCartas = new List<Sprite>();

    [SerializeField] protected Image snake;
    private float snakeStartingPos;
    [SerializeField] private float snakeEndPos;

    LTDescr snakeTween;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

    }
    void Start()
    {
        for (int i = 0; i < letters.Count; i++)
        {
            busyLetters.Add(false);
            letters[i].SetActive(false);
        }

        if (snake != null)
        {
            snakeStartingPos = snake.transform.position.x;
        }
        snakeTween = LeanTween.moveX(snake.gameObject, snakeEndPos, GameManager.instance.totalTimeToRagnarok);
    }

    public void setInitialState(List<Mensaje> mensajes)
    {
        for (int i = 0;i < mensajes.Count;i++)
        {
            //Debug.Log(i);
            if (mensajes[i] != null && !mensajes[i].isDestroyed())
            {
                busyLetters[mensajes[i].getID()] = true;
                changeLetterState(mensajes[i].getID(), mensajes[i].getEstado());
                letters[mensajes[i].getID()].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //snake.transform.position = new Vector3(Mathf.Lerp(snakeStartingPos, snakeEndPos, 1f - GameManager.instance.getRemainingTimePortion()), snake.transform.position.y, 0f);
    }

    public int getFreeLetterSpace()
    {
        for(int i = 0; i < letters.Count; i++)
        {
            if (!busyLetters[i])
            {
                letters[i].SetActive(true);
                changeLetterState(i, 0);
                busyLetters[i] = true;
                return i;
            }
        }
        return -1;
    }

    public void deleteLetter(int im)
    {
        letters[im].SetActive(false);
        busyLetters[im] = false;
    }

    public void changeLetterState(int id, int estado)
    {
        //Debug.Log("cambiando estado desde UI");
        letters[id].GetComponent<Image>().sprite = estadosCartas[estado];
        //Debug.Log(estadosCartas[estado]);
    }


    // Devuelve la posici�n en x de la serpiente que representa la cantidad de tiempo restante.
    private float GetSnakeX()
    {
        return snakeStartingPos + (snakeEndPos - snakeStartingPos) * (1 - GameManager.instance.GetRemainingTimePortion());
    }

    public void DelaySnake()
    {
        float snakeRecoilDuration = 0.5f;
        LeanTween.cancel(snakeTween.id);
        LeanTween.moveX(snake.gameObject, GetSnakeX(), snakeRecoilDuration).setEase(LeanTweenType.easeOutQuad);
        snakeTween = LeanTween.moveX(snake.gameObject, snakeEndPos, GameManager.instance.totalTimeToRagnarok).setDelay(snakeRecoilDuration);
    }
}
