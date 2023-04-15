using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float Score = 0;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        BasicEnemyController.OnEnemyDied += IncreaseScore;
    }

    private void OnDisable()
    {
        BasicEnemyController.OnEnemyDied -= IncreaseScore;
    }

    void IncreaseScore()
    {
        Score++;
    }
}
