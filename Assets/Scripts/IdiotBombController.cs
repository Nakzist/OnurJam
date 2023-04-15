using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;


public class IdiotBombController : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _enemyMoveSpeed;
    
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _health;
    
    [SerializeField]
    private float _damage;

    private bool canMove  =true;
    private bool isAttacking  =false;
    [SerializeField] private LayerMask playerMask;

    [SerializeField] private GameObject player;

    public static Action OnEnemyDied;
    private void Update()
    {
        if (canMove)
        {
            GetComponent<Rigidbody2D>().velocity =
                (player.transform.position - transform.position).normalized * _enemyMoveSpeed * Time.deltaTime;
        }

        if (Physics2D.OverlapCircle(transform.position, 2f, playerMask) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    public void TakeDamage(float damage)
    {
        Die();
    }

    void Die()
    {
        var list = Physics2D.OverlapCircleAll(transform.position, 2f);
        if (list != null)
        {
            foreach (var collider in list)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    collider.TryGetComponent(out IdiotBombController IdiotBombController);
                    if (IdiotBombController == this) continue;

                    damageable.TakeDamage(_damage);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.0f);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        canMove = false;
        yield return new WaitForSeconds(1f);
        Die();
    }
}
