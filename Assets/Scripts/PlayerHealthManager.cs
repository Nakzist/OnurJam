using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100;
    private float _health;

    public static Action OnPlayerDie;

    private bool canTakeDamage = true;
    private void Start()
    {
        _health = _maxHealth;
    }


    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
        {
            return;
        }

        canTakeDamage = false;
        Invoke("SetCanTakeDamage", .5f);
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void SetCanTakeDamage()
    {
        canTakeDamage = true;
    }

    private void Die()
    {
        OnPlayerDie?.Invoke();
    }
}
