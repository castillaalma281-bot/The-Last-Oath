using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public string attackTrigger = "Attack";

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(attackTrigger);
        }
    }
}
