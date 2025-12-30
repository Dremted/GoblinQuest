using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPatrol : MonoBehaviour
{
    [SerializeField] private Transform nextPoint;
    [SerializeField] private float timerIdleMax = 2f;

    private float timerIdle;
    private MoveEnemy currentEnemy;

    private void Update()
    {
        if (currentEnemy == null) return;

        timerIdle += Time.deltaTime;

        if (timerIdle >= timerIdleMax)
        {
            currentEnemy.SetNextPoint(nextPoint);
            currentEnemy.SetEnemyState(EnemyState.Patrol);

            timerIdle = 0f;
            currentEnemy = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MoveEnemy moveEnemy)) return;

        if (moveEnemy.GetEnemyState() == EnemyState.Sleep) return;

        currentEnemy = moveEnemy;
        timerIdle = 0f;

        moveEnemy.SetEnemyState(EnemyState.Idle);
    }
}

