using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        for(int i = 0; i < letters.Count; i++)
        {
            busyLetters.Add(false);
        }
    }

    void setInitialState(List<int> estados)
    {

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
}
