using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgScroll : MonoBehaviour
{
    [SerializeField]
    Vector2 scrollDirection;

    [SerializeField]
    float scrollSpeed;

    bool scrolling;

    RawImage img;

    // En el caso de que se hagan varias llamadas a pausar el scroll, esto asegura que no se retoma hasta que caduquen todas las pausas
    private int pauseCounter;

    private void Start()
    {
        img = GetComponent<RawImage>();
        scrolling = true;
    }

    void Update()
    {
        if (scrolling)
            img.uvRect = new Rect(img.uvRect.position + scrollDirection.normalized * scrollSpeed * Time.deltaTime, img.uvRect.size);
    }

    void pauseScroll(float seconds)
    {
        ++pauseCounter;
        scrolling = false;
        StartCoroutine(restoreScroll(seconds));
    }

    IEnumerator restoreScroll(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (--pauseCounter <= 0) scrolling = true;
    }
}
