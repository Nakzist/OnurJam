using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _enemyMoveSpeed;
    
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _health;
    
    [SerializeField]
    private float _damage;
    
    [SerializeField]
    private float _attackSpeed;

    private bool isAttacking = false;

    [SerializeField] private GameObject player;

    public static Action OnEnemyDied;
    private void Update()
    {
        if (!isAttacking)
        {
            GetComponent<Rigidbody2D>().velocity =
                (player.transform.position - transform.position).normalized * _enemyMoveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && isAttacking)
        {
            StartCoroutine(Attack(col));
        }
    }

    IEnumerator Attack(Collider2D col)
    {
        isAttacking = true;
        col.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(_damage);

        yield return new WaitForSeconds(1 / _attackSpeed);

        isAttacking = false;
    }
}
