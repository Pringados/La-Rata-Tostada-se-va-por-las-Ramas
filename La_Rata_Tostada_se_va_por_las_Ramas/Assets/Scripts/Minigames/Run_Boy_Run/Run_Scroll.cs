using UnityEngine;

public class Run_Scroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed; 

    void Update()
    {
        transform.Translate(Vector2.right * scrollSpeed * Time.deltaTime); 
    }
}
