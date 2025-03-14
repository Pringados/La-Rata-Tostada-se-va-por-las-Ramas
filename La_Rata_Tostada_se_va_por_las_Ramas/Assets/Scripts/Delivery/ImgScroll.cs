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
    public float scrollSpeed;

    [SerializeField]
    public bool scrolling;

    SpriteRenderer img;

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
}
