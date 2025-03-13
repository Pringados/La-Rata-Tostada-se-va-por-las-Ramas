using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ImgScroll : MonoBehaviour
{
    [SerializeField]
    Vector2 scrollDirection;

    [SerializeField]
    float scrollSpeed;

    [SerializeField]
    bool scrolling;

    SpriteRenderer img;

    // En el caso de que se hagan varias llamadas a pausar el scroll, esto asegura que no se retoma hasta que caduquen todas las pausas
    private int pauseCounter;

    Material mat;

    private void Start()
    {
        img = GetComponent<SpriteRenderer>();
        scrolling = true;

        mat = img.material;
    }

    void Update()
    {
        if (scrolling)
        {
            mat.mainTextureOffset += new Vector2 (scrollDirection.x, -scrollDirection.y).normalized * scrollSpeed * Time.deltaTime;
        }
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
