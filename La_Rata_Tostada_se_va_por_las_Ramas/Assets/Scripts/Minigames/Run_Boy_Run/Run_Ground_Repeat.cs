using UnityEngine;

public class Run_Ground_Repeat : MonoBehaviour
{
    [SerializeField] private GameObject squirrel; 

    private float width; 

    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        width = collider.size.x;
    }

    void Update()
    {
        if (squirrel.transform.position.x > transform.position.x + width / 2)
            transform.Translate(Vector2.right * 2f * width); 
    }
}
