using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;            // Distancia del ataque
    public int attackDamage = 10;             // Daño que inflige
    public float attackCooldown = 1f;         // Tiempo entre ataques
    public LayerMask enemyLayer;              // Qué capas se consideran enemigos

    private float lastAttackTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Opcional: activar animación
        // GetComponent<Animator>().SetTrigger("Attack");

        // Detectar enemigos en el rango
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            // Aplicar daño si el enemigo tiene el script adecuado
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    // Visualización del rango en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
