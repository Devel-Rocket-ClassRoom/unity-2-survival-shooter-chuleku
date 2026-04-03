using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IDamageAble
{
    public float startingHealth = 100f;
    public float health { get; private set; }
    public bool isDead { get; private set; }

    public UnityEvent onDead;

    protected virtual void OnEnable()
    {
        health = startingHealth;
        isDead = false;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Die();
        }

    }

    public virtual void Die()
    {
        isDead = true;
        onDead?.Invoke();
    }
    public virtual void RestoreHealth(float newHealth)
    {
        if (isDead) return;

        health += newHealth;

        if (health > startingHealth)
        {
            health = startingHealth;
        }
    }
    public void Heal(float heal)
    {
        health += heal;
        if (health > startingHealth)
        {
            health = startingHealth;
        }
    }
}
