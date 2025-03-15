using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] protected Slider snake;

    [SerializeField] private float increaseTime;
    private float time; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

    }
    void Start()
    {
        for(int i = 0; i < letters.Count; i++)
        {
            busyLetters.Add(false);
            letters[i].SetActive(false);
        }

        if (snake != null)
        {
            snake.value = GameManager.instance.getTimeToRagnarok();

            time = increaseTime;
        }
    }

    public void setInitialState(List<Mensaje> mensajes)
    {
        for (int i = 0;i < mensajes.Count;i++)
        {
            Debug.Log(i);
            if (mensajes[i] != null && !mensajes[i].isDestroyed())
            {
                busyLetters[mensajes[i].getID()] = true;
                changeLetterState(mensajes[i].getID(), mensajes[i].getEstado());
                letters[mensajes[i].getID()].SetActive(true);
            }
        }

        //Debug.Log("estado inicializado");
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime; 

        if (time < 0f)
        {
            time = increaseTime;

            GameManager.instance.increaseTimeToRagnarok(0.05f);

            if (snake != null)
                snake.value += 0.05f; 
        }
    }

    public int getFreeLetterSpace()
    {
        for(int i = 0; i <letters.Count; i++)
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
}
