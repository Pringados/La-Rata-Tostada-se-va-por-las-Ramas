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

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image getFreeLetterSpace()
    {
        for(int i = 0; i <letters.Count; i++)
        {
            if (!busyLetters[i])
                return letters[i].GetComponent<Image>();
        }
        return null;
    }
}
