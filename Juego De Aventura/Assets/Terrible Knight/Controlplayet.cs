using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController2D_MoveOnly_Full : MonoBehaviour
{
    [Header("Velocidad")]
    public float moveSpeed = 5f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private Vector2 moveInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput.actions == null)
        {
            Debug.LogError("❌ PlayerInput no tiene un InputActions asset asignado");
            return;
        }

        moveAction = playerInput.actions["Move"];

        if (moveAction == null)
            Debug.LogError("❌ No se encontró la acción 'Move'.");
        else
            Debug.Log("✅ Acción 'Move' encontrada correctamente");
    }

    void Update()
    {
        if (moveAction != null)
        {
            moveInput = moveAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f) * moveSpeed * Time.deltaTime;
            transform.position += move;
        }
    }
}
