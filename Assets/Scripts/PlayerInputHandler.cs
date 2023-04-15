using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler instance;

    private void Awake()
    {
        instance = this;
    }
    
    public bool IsDashButtonPressed()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    
    public float GetHorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVerticalInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public bool IsShooting()
    {
        return Input.GetButton("Fire1");
    }
}
