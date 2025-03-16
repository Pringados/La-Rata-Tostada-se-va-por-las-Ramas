using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RastroFacundo : MonoBehaviour
{

    void Start()
    {
        Vector3 finalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        LeanTween.scale(this.gameObject, finalScale, 0.5f).setEase(LeanTweenType.easeOutBack);
    }

    private void OnMouseExit()
    {
        if (Input.GetMouseButton(0))
            Destroy(gameObject);
    }

}
