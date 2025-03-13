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

    bool scrolling;

    SpriteRenderer img;

    Vector3 startingPos;

    // En el caso de que se hagan varias llamadas a pausar el scroll, esto asegura que no se retoma hasta que caduquen todas las pausas
    private int pauseCounter;

    [SerializeField, Tooltip("Gameobjects auxiliares que se usan para crear las extensiones del sprite. Deben ser hijos vacíos de este objeto.")]
    GameObject child1;
    [SerializeField, Tooltip("Gameobjects auxiliares que se usan para crear las extensiones del sprite. Deben ser hijos vacíos de este objeto.")]
    GameObject child2;

    private void Start()
    {
        img = GetComponent<SpriteRenderer>();
        scrolling = true;
        startingPos = transform.position;

        if (scrollDirection.x > 0)
        {
            child1.AddComponent(img);
            child1.transform.position = new Vector3(transform.position.x - img.bounds.size.x, transform.position.y, transform.position.z);
        }
        else if (scrollDirection.x < 0)
        {
            child1.AddComponent(img);
            child1.transform.position = new Vector3(transform.position.x + img.bounds.size.x, transform.position.y, transform.position.z);
        }

        if (scrollDirection.y > 0)
        {
            child1.AddComponent(img);
            child1.transform.position = new Vector3(transform.position.x, transform.position.y - img.bounds.size.y, transform.position.z);
        }
        else if (scrollDirection.y < 0)
        {
            child1.AddComponent(img);
            child1.transform.position = new Vector3(transform.position.x, transform.position.y + img.bounds.size.y, transform.position.z);
        }
    }

    void Update()
    {
        if (scrolling)
        {
            transform.position = new Vector3 (transform.position.x + scrollDirection.normalized.x * scrollSpeed * Time.deltaTime, 
                transform.position.y + scrollDirection.normalized.y * scrollSpeed * Time.deltaTime, 0f);

            if (Math.Abs(transform.position.x - startingPos.x) > img.bounds.size.x)
            {
                if (transform.position.x > startingPos.x)
                    transform.position = new Vector3(transform.position.x - img.bounds.size.x, transform.position.y, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x + img.bounds.size.x, transform.position.y, transform.position.z);
            }

            if (Math.Abs(transform.position.y - startingPos.y) > img.bounds.size.y)
            {
                if (transform.position.y > startingPos.y)
                    transform.position = new Vector3(transform.position.x, transform.position.y - img.bounds.size.y, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x, transform.position.y + img.bounds.size.y, transform.position.z);
            }
        }
            //img.uvRect = new Rect(img.uvRect.position + scrollDirection.normalized * scrollSpeed * Time.deltaTime, img.uvRect.size);
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
