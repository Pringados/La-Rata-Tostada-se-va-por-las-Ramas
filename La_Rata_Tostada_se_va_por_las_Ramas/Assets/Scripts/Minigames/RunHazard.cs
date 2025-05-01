using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHazard : MonoBehaviour
{
    Run_Player player;

    private float widthPerHeight = 6f; // Valores más altos de esta variable hacen que los obstáculos pierdan anchura más agresivamente conforme incrementa su altura
    private float minHeight = -10.5f, maxHeight = -4.5f, minWidth = 0.4f, maxWidth = 1.6f;

    private void Awake()
    {
        Reset();
    }

    private void Start()
    {
        transform.parent.GetComponent<Run_Ground_Repeat>().hazards.Add(this);
    }

    public void Reset()
    {
        float heightSlider = Random.Range(0f, 1f);  // Determina la altura y anchura del obstáculo; a más altura, menos anchura
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(minHeight, maxHeight, heightSlider), transform.localPosition.z);
        transform.localScale = new Vector3(Mathf.Lerp(minWidth, maxWidth, 4 / Mathf.Pow(widthPerHeight * heightSlider, 2)), transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Run_Player>();
        if (player != null)
            player.Reset();
    }
}
