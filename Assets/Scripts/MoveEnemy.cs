using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float openDoorSpeed = 3f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private EnemyState currentState;
    [SerializeField] private Transform callPoint;
    
    private Transform nextVetticalDoor;
    private Vector2 moveDir;
    private Transform nextPoint;
    private Rigidbody2D rb;
    public EnemyState CurrentState => currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.UseVerticalDoor:
                UseVerticalDoor();
                break;
            case EnemyState.Sleep:
                rb.velocity = Vector2.zero;

                break;
            case EnemyState.Idle:
                rb.velocity = Vector2.zero;

                break;
            case EnemyState.UseDoor:
                UseDoor();
                break;
            case EnemyState.Patrol:
                Move();
                break;
            case EnemyState.Call:
                Call();
                break;
        }
    }

    private void Move()
    {
        moveDir = nextPoint.position - transform.position;
        rb.velocity = moveDir.normalized * MoveSpeed;

        RotateEnemy();
    }

    private void UseDoor()
    {
        if (nextPoint != null)
        {
            moveDir = nextPoint.position - transform.position;
            rb.velocity = moveDir.normalized * openDoorSpeed;
        }
        RotateEnemy();
    }

    private void Call()
    {
        if(callPoint != null)
        {
            nextPoint = null;
            moveDir = callPoint.position - transform.position;
            rb.velocity = moveDir.normalized * runSpeed;
        }
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

    public void SetNextCallPoint(Transform point)
    {
        callPoint = point;
    }

    public EnemyState GetEnemyState()
    {
        return currentState;
    }

    private void UseVerticalDoor()
    {
        transform.position = nextVetticalDoor.position;
    }

    public void SetNextVerticalDoor(Transform point)
    {
        nextVetticalDoor = point;
    }
}
public enum EnemyState
{
    Idle = 0,
    Patrol = 1,
    Gotcha = 2,
    UseDoor = 3,
    Sleep = 4,
    Call = 5,
    UseVerticalDoor = 6
}
