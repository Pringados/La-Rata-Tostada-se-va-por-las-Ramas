using UnityEngine;
using UnityEngine.UI;

public class Demons_Buttons : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private float minTime;

    [SerializeField] private Sprite hole;
    [SerializeField]  private Sprite demon;
    [SerializeField] private float spawnTime;

    private RectTransform rectTransform;

    private float resetTime;
    private float time;

    private bool active;

    void Start()
    {
        this.GetComponent<Image>().sprite = demon;

        rectTransform = this.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 90);

        active = (Random.Range(0, 2) % 2 == 0);

        if (!active)
        {
            //this.GetComponent<Image>().sprite = hole;
            //rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 20);
            LeanTween.move(gameObject, rectTransform.position - new Vector3(0, rectTransform.rect.height * 2 +60), 0).setOnComplete(hideDemon);
        }

        this.GetComponent<Button>().interactable = active;

        resetTime = Random.Range(minTime, maxTime);

        time = resetTime; 
    }

    void Update()
    {
        time -= Time.deltaTime; 

        if (time <= 0f)
        {
            if (active)
            {
                active = false;
                LeanTween.move(gameObject, rectTransform.position - new Vector3(0, rectTransform.rect.height*2 +60), spawnTime).setOnComplete(hideDemon);
                //hideDemon();
            }

            else
            {
                active = true;
                LeanTween.move(gameObject, rectTransform.position + new Vector3(0, rectTransform.rect.height * 2+60), spawnTime).setOnComplete(showDemon);

            }

            this.GetComponent<Button>().interactable = active;

            time = resetTime; 
        }
    }

    public void ThisDemonClick()
    {
        active = false;
        LeanTween.move(gameObject, rectTransform.position - new Vector3(0, rectTransform.rect.height * 2+60), 0).setOnComplete(hideDemon);
        //this.GetComponent<Image>().sprite = hole;

        //rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 20);

        //this.GetComponent<Button>().interactable = false;
    }

    void hideDemon()
    {
        //this.GetComponent<Image>().sprite = hole;
        //para devolverlo a su sitio
       // LeanTween.move(gameObject, rectTransform.position + new Vector3(0, rectTransform.rect.height), 0);
        //rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 20);
    }
    void showDemon()
    {
        //this.GetComponent<Image>().sprite = demon;

       // rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 90);

    }
}
