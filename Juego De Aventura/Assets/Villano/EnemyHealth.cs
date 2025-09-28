using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
     public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Desactivar enemigo o reproducir animación de muerte
        Destroy(gameObject);
    }
}
