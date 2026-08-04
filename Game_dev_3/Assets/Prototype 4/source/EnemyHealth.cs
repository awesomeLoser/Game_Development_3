using UnityEngine;

// Minimal stub so Slash() has something to call TakeDamage() on.
// Attach to any enemy on a layer included in the player's "enemyLayers" mask.
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    int currentHealth;

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
        Destroy(gameObject);
    }
}