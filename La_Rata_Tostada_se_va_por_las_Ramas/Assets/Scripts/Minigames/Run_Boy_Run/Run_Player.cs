using UnityEngine;

public class Run_Player : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float radius;
    [SerializeField] private float scrollSpeed;

    [SerializeField] private LayerMask ground;

    [SerializeField] private Transform groundCheck;

    private Rigidbody2D rb2D;

    private float distanceGround;

    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * scrollSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);

            if (isGrounded)
            {
                rb2D.AddForce(Vector3.up * force);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius); 
    }
}
