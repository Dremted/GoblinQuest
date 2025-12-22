using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;

    private Transform nextPoint;
    private Rigidbody2D rb;
    private EnemyState currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = EnemyState.Idle;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                rb.velocity = Vector3.zero;
                break;
            case EnemyState.Patrol:
                Move();
                break;
        }
    }

    private void Move()
    {
        Vector3 moveDir = nextPoint.position - transform.position ;
        rb.velocity = moveDir.normalized * MoveSpeed;
    }

    public void SetEnemyState(EnemyState enemyState)
    {
        currentState = enemyState;
    }

    public void SetNextPoint(Transform point)
    {
        nextPoint = point;
    }

}

public enum EnemyState
{
    Idle,
    Patrol,
    Gotcha
}
