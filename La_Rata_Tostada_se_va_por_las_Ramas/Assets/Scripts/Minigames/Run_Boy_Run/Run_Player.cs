using UnityEngine;

public class Run_Player : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float radius;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask ground;

    private Rigidbody2D rb2D; 

    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground); 

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
                rb2D.AddForce(Vector2.up * force);
        }
    }
}
