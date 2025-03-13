using UnityEngine;
using UnityEngine.UI;

public class Demons_Buttons : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private float minTime;

    private float resetTime;
    private float time;

    private bool active; 

    void Start()
    {
        this.GetComponent<Image>().enabled = true;

        this.GetComponent<Button>().interactable = true;

        active = (Random.Range(0, 2) % 2 == 0); 

        if (!active)
        {
            this.GetComponent<Image>().enabled = false;

            this.GetComponent<Button>().interactable = false; 
        }

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

                this.GetComponent<Image>().enabled = false;

                this.GetComponent<Button>().interactable = false;
            }

            else
            {
                active = true;

                this.GetComponent<Image>().enabled = true;

                this.GetComponent<Button>().interactable = true;
            }

            time = resetTime; 
        }
    }

    public void ThisDemonClick()
    {
        active = false;

        this.GetComponent<Image>().enabled = false;

        this.GetComponent<Button>().interactable = false;
    }
}
