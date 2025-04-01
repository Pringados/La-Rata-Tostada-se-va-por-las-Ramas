using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UI;

public class Run_Player : IMinigame
{
    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float radius;

    [SerializeField] private LayerMask ground;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject butt;


    private Rigidbody2D rb2D;

    private Vector3 startPos;
    private bool moving = true;
    private Animator animator;

    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();

        butt.GetComponent<Button>().onClick.AddListener(win);

        startPos = transform.position;

        animator = GetComponent<Animator>();
        rb2D.velocity = new Vector3(speed, rb2D.velocity.y, 0f);
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);

        animator.enabled = isGrounded;

        if (Input.GetMouseButtonDown(0))
        {
            if (isGrounded)
            {
                rb2D.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            rb2D.velocity = new Vector3(rb2D.velocity.x, Mathf.Min(0f, rb2D.velocity.y), 0f);
            rb2D.AddForce(Vector3.down * force, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //if (moving) rb2D.velocity = new Vector3(speed, rb2D.velocity.y, 0f);
        //else rb2D.velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    public void win()
    {
        GameManager.instance.increaseTimeToRagnarok(50 / 100);
        GameManager.instance.score += 50;
        GameManager.instance.OpenMapScene();
    }

    public void Reset()
    {
        transform.position = startPos;
    }

    public override float CalculateScore()
    {
        return 500;
    }
}
