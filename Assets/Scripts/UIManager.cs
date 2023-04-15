using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _deathPanel;

    private void OnEnable()
    {
        PlayerHealthManager.OnPlayerDie += ActivateDeathPanel;
    }
    
    private void OnDisable()
    {
        PlayerHealthManager.OnPlayerDie -= ActivateDeathPanel;
    }

    void ActivateDeathPanel()
    {
        _deathPanel.SetActive(true);
    }
}
