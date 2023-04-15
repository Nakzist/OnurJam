using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    private float _nextShootingTime = 0;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPointTransform;
    
    private void Update()
    {
        if (_nextShootingTime <= Time.time)
        {
            _nextShootingTime = Time.time + delay;
            GameObject _bullet = Instantiate(_bulletPrefab, _bulletSpawnPointTransform.position, Quaternion.identity);
            _bullet.GetComponent<Rigidbody2D>().velocity = transform.up * 10f;
        }
    }
}
