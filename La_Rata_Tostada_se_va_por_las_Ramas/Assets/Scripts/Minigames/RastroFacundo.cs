using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RastroFacundo : MonoBehaviour
{
    Collider2D col;
    Vector3 finalScale;

    public FacundoManager manager;

    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
        finalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        LeanTween.scale(this.gameObject, finalScale, 0.3f).setEase(LeanTweenType.easeOutBack).setOnComplete(delegate(){
        col.enabled = true;
        });
        manager.AddTrailObject(this);
    }

    private void OnMouseExit()
    {
        //if (Input.GetMouseButton(0))
        //    Destroy(gameObject);
    }

    public void RemoveTrail()
    {
        Debug.Log("RemoveTrail");
        Debug.Log("TimeScale: " + Time.timeScale);
        //col.enabled = false;
        //LeanTween.scale(this.gameObject, Vector3.zero, Random.Range(0.1f, 0.2f)).setEase(LeanTweenType.easeInBack).setDelay(Random.Range(0f, 0.5f))
        //    .setOnComplete(delegate () { Debug.Log("Callback");  Destroy(gameObject); });
        //LeanTween.scale(this.gameObject, Vector3.one * 3, 1f);
        //transform.LeanScale(Vector3.zero, 1f);
        StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        float shrinkTime = Random.Range(0.05f, 0.15f), shrinkDelay = Random.Range(0f, 0.2f);
        float currShrinkTime = shrinkTime;
        while (shrinkDelay > 0)
        {
            shrinkDelay -= Time.deltaTime;
            yield return null;
        }
        while (currShrinkTime > 0)
        {
            currShrinkTime -= Time.deltaTime;
            Debug.Log("Shrinking");
            transform.localScale = finalScale * currShrinkTime / shrinkTime;
            yield return null;
        }
        manager.RemoveTrailObject(this);
        Destroy(this.gameObject);
    }
}
