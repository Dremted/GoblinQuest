using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float openDoorSpeed;

    private Vector2 moveDir;
    private Transform nextPoint;
    private Rigidbody2D rb;
    private EnemyState currentState;

    public bool IsWalking { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = EnemyState.Idle;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
            case EnemyState.UseDoor:

                IsWalking = false;
                break;
            case EnemyState.Patrol:
                Move();
                break;
        }
    }

    private void Move()
    {
        moveDir = nextPoint.position - transform.position;
        rb.velocity = moveDir.normalized * MoveSpeed;
        IsWalking = true;
        RotateEnemy();
    }

    private void RotateEnemy()
    {

        if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void SetEnemyState(EnemyState enemyState)
    {
        currentState = enemyState;
    }

    public void SetNextPoint(Transform point)
    {
        nextPoint = point;
    }

    public EnemyState GetEnemyState()
    {
        return currentState;
    }

}

public enum EnemyState
{
    Idle,
    Patrol,
    Gotcha,
    UseDoor
}
