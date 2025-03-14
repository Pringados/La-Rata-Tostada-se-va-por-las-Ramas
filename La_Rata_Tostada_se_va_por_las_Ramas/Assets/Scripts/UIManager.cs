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
    }

    public void setInitialState(List<Mensaje> mensajes)
    {
        for (int i = 0;i < mensajes.Count;i++)
        {
            if (mensajes[i] != null && !mensajes[i].isDestroyed())
            {
                busyLetters[mensajes[i].getID()] = true;
                changeLetterState(mensajes[i].getID(), mensajes[i].getEstado());
                letters[mensajes[i].getID()].SetActive(true);
            }
        }

        Debug.Log("estado inicializado");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getFreeLetterSpace()
    {
        for(int i = 0; i <letters.Count; i++)
        {
            if (!busyLetters[i])
            {
                //resetear el sprite de ruptura o desde fuera me la pela
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
        Debug.Log("cambiando estado desde UI");
        letters[id].GetComponent<Image>().sprite = estadosCartas[estado];
        Debug.Log(estadosCartas[estado]);
    }
}
