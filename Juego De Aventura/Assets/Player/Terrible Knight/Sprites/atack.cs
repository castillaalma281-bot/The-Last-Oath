using UnityEngine;

public class atack : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Verificar si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (!isAttacking)
        {
            Move();
            Jump();
        }

        Attack();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        if (moveX != 0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(moveX > 0 ? 1 : -1, 1, 1);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("jump");
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F) && isGrounded)
        {
            isAttacking = true;
            animator.SetTrigger("attack");
            Invoke(nameof(ResetAttack), 0.6f); // duración del ataque
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    // Visualizar el área del suelo
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }
}