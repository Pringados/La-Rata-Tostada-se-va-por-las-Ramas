using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientTransition : MonoBehaviour
{
    [SerializeField]
    float introFadeDuration, outroFadeDuration;
    private SpriteRenderer sprite;
    void Start()
    {
        LeanTween.moveY(this.gameObject, -1171f, introFadeDuration).setEase(LeanTweenType.easeOutSine);
    }

    public void LevelEnd()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 180f);
        transform.position = new Vector3(transform.position.x, 2232f, 0f);
        LeanTween.moveY(this.gameObject, -41f, outroFadeDuration).setEase(LeanTweenType.easeInSine).setDelay(0.5f);
    }
}
