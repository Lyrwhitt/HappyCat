using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public StatusSO statusData;

    protected float health;

    public event Action onDie;
    public event Action onHealthChange;

    protected virtual void Start()
    {
        health = statusData.hp;
    }

    public void ChangeHealth(float health)
    {
        this.health = health;
        health = Mathf.Clamp(health, 0f, statusData.maxHP);

        onHealthChange?.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);

        onHealthChange?.Invoke();

        if (health == 0)
            onDie?.Invoke();
    }

    public virtual void RecoveryHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0f, statusData.maxHP);

        onHealthChange?.Invoke();
    }

    public float GetPercentageHP()
    {
        return health / statusData.maxHP;
    }
}
