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
    //[SerializeField]
    //Canvas canvas;
    [SerializeField] GameObject canvas; 
    [SerializeField]
    int points;

    [SerializeField] float offsetinY;
    [SerializeField] float paddinginx;

    int objToFind;

    [SerializeField]
    List<Sprite> instanciableObjects = new List<Sprite>();

    [SerializeField]
    GameObject whatToFind;

    [SerializeField]
    GameObject forbiddenSquare;
    Vector2 forbIni, forbFin;

    // Start is called before the first frame update
    void Start()
    {
        canvaX = canvas.GetComponent<RectTransform>().rect.width / 2;
        canvaY = canvas.GetComponent<RectTransform>().rect.height / 2;
        //primero decidimos q objeto vamos a encontrar:
        objToFind = Random.Range(0, instanciableObjects.Count);

        //calculamos el cuadrado donde no se puede spawnear
        RectTransform forbTrans = forbiddenSquare.GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        forbTrans.GetWorldCorners(corners);
        //forbIni = corners[0];

        Vector2 forbLeftBottom, forbRightTop;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), corners[0], canvas.transform.GetComponentInParent<Canvas>().worldCamera, out forbLeftBottom);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), corners[2], canvas.transform.GetComponentInParent<Canvas>().worldCamera, out forbRightTop);

        forbIni.x = forbLeftBottom.x;
        forbIni.y = forbRightTop.y;
        Debug.Log(forbIni);

        //instanciamos todos los objetos en posiciones Random
        for (int i = 0; i < instanciableObjects.Count; i++)
        {
            if (i != objToFind)
            {
                int rep = Random.Range(minObjects, maxObjects);
                for (int j = 0; j < rep; j++)
                {
                    Vector2 pos = new Vector2(Random.Range(-canvaX + paddinginx, canvaX - paddinginx), Random.Range(-canvaY+10, -offsetinY));
                    while (pos.x >= forbIni.x && pos.y <= forbIni.y)    //si está en el forbREct
                    {
                        pos = new Vector2(Random.Range(-canvaX + paddinginx, canvaX - paddinginx), Random.Range(-canvaY+10, -offsetinY));
                    }
                    Debug.Log(pos);
                    GameObject newButton = new GameObject();
                    RectTransform trans = newButton.AddComponent<RectTransform>();

                    newButton.AddComponent<CanvasRenderer>();
                    trans.SetParent(canvas.transform);
                    trans.anchoredPosition = pos;

                    Image img = newButton.AddComponent<Image>();
                    img.sprite = instanciableObjects[i];
                }

            }
        }

        //instanciamos el único botón (y su representación en la esquina)
        GameObject objButt = new GameObject();
        RectTransform transform = objButt.AddComponent<RectTransform>();

        Vector2 p = new Vector2(Random.Range(-canvaX + paddinginx, canvaX - paddinginx), Random.Range(-canvaY + 10, -offsetinY));
        while (p.x >= forbIni.x && p.y <= forbIni.y)    //si está en el forbREct
        {
            p = new Vector2(Random.Range(-canvaX + paddinginx, canvaX - paddinginx), Random.Range(-canvaY + 10, -offsetinY));
        }

        objButt.AddComponent<CanvasRenderer>();
        transform.SetParent(canvas.transform);
        transform.anchoredPosition = p;

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
