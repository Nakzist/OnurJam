using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private Vector2 _velocity;
    [SerializeField] private float _moveSpeed;

    private Vector2 _mousePosition;
    private Rigidbody2D _rigidBody2D;
    
    public static float bulletDamage = 5f;
    
    [Header("Dash Settings")]
    private bool _isDashing;
    private bool _canDash = true;
    [SerializeField] private float _dashTime = 0.25f;
    [SerializeField] private float _dashCooldown = 20f;
    [SerializeField] private float _dashSpeed;
    private TrailRenderer _trailRenderer;

    private void Start()
    {
        TryGetComponent(out _rigidBody2D);
        TryGetComponent(out _trailRenderer);
    }

    private void Update()
    {
        //Setting velocity and mouse position depending on player inputs
        _velocity = new Vector2(PlayerInputHandler.instance.GetHorizontalInput(),
            PlayerInputHandler.instance.GetVerticalInput()) ;
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_canDash && PlayerInputHandler.instance.IsDashButtonPressed())
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!_isDashing)
        {
            GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + _velocity * _moveSpeed * Time.fixedDeltaTime);
        }
        Vector2 _lookDirection = _mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0,0, angle);
    }

    IEnumerator Dash()
    {
        Debug.Log("Dash");
        _canDash = false;
        _isDashing = true;
        _trailRenderer.emitting = true;
        _rigidBody2D.velocity = _velocity * _dashSpeed;
        yield return new WaitForSeconds(_dashTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
        _rigidBody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(_dashCooldown - _dashTime);
        _canDash = true;
        Debug.Log("Dash End");
    }
}
