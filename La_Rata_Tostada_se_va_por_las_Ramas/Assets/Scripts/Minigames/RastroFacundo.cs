using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RastroFacundo : MonoBehaviour
{
    Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
        Vector3 finalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        LeanTween.scale(this.gameObject, finalScale, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(delegate(){
        col.enabled = true;
        });
    }

    private void OnMouseExit()
    {
        //if (Input.GetMouseButton(0))
        //    Destroy(gameObject);
    }

}
