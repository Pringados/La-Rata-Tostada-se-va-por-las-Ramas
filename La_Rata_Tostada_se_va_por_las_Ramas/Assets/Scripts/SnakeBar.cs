using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBar : MonoBehaviour
{
    private float snakeStartingPos;
    [SerializeField] private float snakeEndPos;

    LTDescr snakeTween;

    void Start()
    {
        snakeStartingPos = transform.position.x;
        transform.position = new Vector3(GetSnakeX(), transform.position.y, transform.position.z);
        snakeTween = LeanTween.moveX(gameObject, snakeEndPos, GameManager.instance.remainingTimeToRagnarok);
        GameManager.instance.SetSnake(this);
    }

    void Update()
    {
        
    }

    public void DelaySnake()
    {
        float snakeRecoilDuration = 0.5f;
        LeanTween.cancel(snakeTween.id);
        LeanTween.moveX(gameObject, GetSnakeX(), snakeRecoilDuration).setEase(LeanTweenType.easeOutQuad);
        snakeTween = LeanTween.moveX(gameObject, snakeEndPos, GameManager.instance.totalTimeToRagnarok).setDelay(snakeRecoilDuration);
    }

    // Devuelve la posici�n en x de la serpiente que representa la cantidad de tiempo restante.
    private float GetSnakeX()
    {
        return snakeStartingPos + (snakeEndPos - snakeStartingPos) * (1 - GameManager.instance.GetRemainingTimePortion());
    }

}
