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

    [SerializeField]
    public bool followCamera;

    Vector3 offsetFromCamera;

    Renderer img;

    Material mat;

    private void Start()
    {
        img = GetComponent<Renderer>();
        scrolling = true;

        mat = img.material;

        offsetFromCamera = transform.position - Camera.main.transform.position;
    }

    private void Update()
    {
        if (followCamera)
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f) + offsetFromCamera;
    }

    void LateUpdate()
    {
        if (scrolling)
        {
            mat.mainTextureOffset += new Vector2 (scrollDirection.x, -scrollDirection.y).normalized * scrollSpeed * Time.deltaTime;
        }
    }
}
