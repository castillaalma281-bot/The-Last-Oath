using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput), typeof(Animator))]
public class PlayerAttackController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ataque")]
    public Villano villano; // Asignar villano en inspector
    public float attackRange = 2f; // Rango para dañar al villano
    public float doublePressThreshold = 0.3f;

    private Rigidbody2D rb;
    private Animator animator;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private Vector2 moveInput;
    private int spacePressCount = 0;
    private float lastSpacePressTime = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        if (playerInput.actions == null)
        {
            Debug.LogError("❌ PlayerInput no tiene un InputActions asset asignado.");
            return;
        }

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        attackAction = playerInput.actions["Attack"];
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
    }

    private void HandleMovement()
    {
        if (moveAction != null)
        {
            moveInput = moveAction.ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void HandleJump()
    {
        if (jumpAction != null && jumpAction.triggered)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void HandleAttack()
    {
        if (attackAction != null && attackAction.triggered)
        {
            float currentTime = Time.time;

            if (currentTime - lastSpacePressTime < doublePressThreshold)
                spacePressCount++;
            else
                spacePressCount = 1;

            lastSpacePressTime = currentTime;

            if (spacePressCount == 2)
            {
                DoAttack();
                spacePressCount = 0;
            }
        }
    }

    private void DoAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("AttackCrouch");
            animator.SetTrigger("SwordSlash");
        }

        if (villano != null && Mathf.Abs(transform.position.x - villano.transform.position.x) <= attackRange)
        {
            villano.RecibirDanio(0.5f);
            Debug.Log("Ataque ejecutado y villano dañado");
        }
        else
        {
            Debug.Log("Ataque ejecutado pero sin villano al frente");
        }
    }
}
