using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingBeing : MonoBehaviour {

    [Header("References")]
    public Rigidbody2D _rigidbody;
    public SpriteRenderer _renderer;

    [Header("Variables")]
    public float startHealth;
    public float currentHealth;

    [Header("Health")]
    public UnityEvent OnHealthFull;
    public UnityEvent OnHealthHalf;
    public UnityEvent OnHealthEmpty;

    protected virtual void Start()
    {
        currentHealth = startHealth;
        OnHealthFull.Invoke();
    }

    public virtual void TakeDamage(float amount) {

        float half = startHealth / 2;
        if(currentHealth > half)
        {
            if(currentHealth - amount < half)
            {
                OnHealthHalf.Invoke();
            }
        }
        currentHealth -= amount;


        if(currentHealth <= 0)
        {
            OnHealthEmpty.Invoke();
            Die();
        }

        UpdateHealthBar();
    }

    public virtual void Heal(int amount)
    {
        if (currentHealth + amount > startHealth)
        {
            float remainder = startHealth - currentHealth;
            currentHealth += remainder;
            OnHealthFull.Invoke();
        }
        else
        {
            currentHealth += amount;
        }

        UpdateHealthBar();

    }

    protected virtual void Die()
    {
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Override this method depending on if the inheriting script is player or enemy.
    }

    protected virtual void UpdateHealthBar()
    {

    }
}
