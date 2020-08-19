using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public HealthBar healthBar;
    public HitAwareness[] hitAwareness;

    Killable killable;
    float currentHealth;

    private void Awake()
    {
        this.killable = this.GetComponent<Killable>();
    }

    void Start()
    {
        this.currentHealth = this.maxHealth;
        this.healthBar.SetMaxHealth(this.maxHealth);
        this.healthBar.SetHealth(this.currentHealth);
    }

    public void TakeDamage(float damage)
    {
        this.currentHealth -= damage;
        this.healthBar.SetHealth(this.currentHealth);

        if (this.currentHealth <= 0)
        {
            this.killable.OnDeath();
        }
        else
        {
            foreach (var h in this.hitAwareness)
            {
                h.NotifyHit();
            }
        }
    }

    public void GainHealth(float healthAmount)
    {
        this.currentHealth = Mathf.Min(this.maxHealth, this.currentHealth + healthAmount);
        this.healthBar.SetHealth(this.currentHealth);
    }
}
