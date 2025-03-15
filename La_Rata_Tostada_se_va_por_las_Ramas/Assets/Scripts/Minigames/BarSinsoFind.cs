using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSinsoFind : IMinigame
{
    [SerializeField]
    int minObjects; 
    [SerializeField]
    int maxObjects;
    float canvaX;
    float canvaY;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    int points;

    int objToFind;

    [SerializeField]
    List<Sprite> instanciableObjects = new List<Sprite>();

    [SerializeField]
    GameObject whatToFind;

    // Start is called before the first frame update
    void Start()
    {
        canvaX = canvas.GetComponent<RectTransform>().rect.width / 2;
        canvaY = canvas.GetComponent<RectTransform>().rect.height / 2;
        //primero decidimos q objeto vamos a encontrar:
        objToFind = Random.Range(0, instanciableObjects.Count);

        //instanciamos todos los objetos en posiciones Random
        for (int i = 0; i < instanciableObjects.Count; i++)
        {
            if (i != objToFind)
            {
                int rep = Random.Range(minObjects, maxObjects);
                for (int j = 0; j < rep; j++)
                {
                    GameObject newButton = new GameObject();
                    RectTransform trans = newButton.AddComponent<RectTransform>();

                    newButton.AddComponent<CanvasRenderer>();
                    trans.SetParent(canvas.transform);
                    trans.anchoredPosition = new Vector2(Random.Range(-canvaX, canvaX), Random.Range(-canvaY, 0));

                    Image img = newButton.AddComponent<Image>();
                    img.sprite = instanciableObjects[i];
                }

            }
        }

        //instanciamos el único botón (y su representación en la esquina)
        GameObject objButt = new GameObject();
        RectTransform transform = objButt.AddComponent<RectTransform>();

        objButt.AddComponent<CanvasRenderer>();
        transform.SetParent(canvas.transform);
        transform.anchoredPosition = new Vector2(Random.Range(-canvaX, canvaX), Random.Range(-canvaY, 0));

        objButt.AddComponent<Image>().sprite = instanciableObjects[objToFind];

        Button button = objButt.AddComponent<Button>();

        whatToFind.GetComponent<Image>().sprite = instanciableObjects[objToFind];

        button.onClick.AddListener(onButtonPush);

    }

    public void onButtonPush()
    {
        MinigameComplete(true);
    }

    override public int CalculateScore() 
    {
        return points;
    }
}
