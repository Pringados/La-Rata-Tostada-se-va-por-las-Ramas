using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using FMOD;


public class SmoothCamera : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] float duration;
    [SerializeField] RectTransform rt;
    [SerializeField] AnimationCurve curva;

    public void MoveDown()
    {
        StartCoroutine(Smoother(distance)); 
    }

    public void MoveUp()
    {
        StartCoroutine(Smoother(-distance));
    }

    private IEnumerator Smoother(float d)
    {
        float time = 0;

        Vector3 iniPos = rt.anchoredPosition;

        Vector3 lastPos = new Vector3(rt.anchoredPosition.x, rt.anchoredPosition.y + d, 0);

        while (time < duration)
        {
            rt.anchoredPosition = Vector3.Lerp(iniPos, lastPos, curva.Evaluate(time / duration));

            time += Time.deltaTime;

            yield return new WaitForSeconds(0.005f);
        }
    }
}
