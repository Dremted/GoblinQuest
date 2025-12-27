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

    private Vector2 moveDir;
    private Transform nextPoint;
    private Rigidbody2D rb;


    public bool IsWalking { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.UseVerticalDoor:
                
            case EnemyState.Sleep:
                rb.velocity = Vector2.zero;
                IsWalking = false;
                break;
            case EnemyState.Idle:
                rb.velocity = Vector2.zero;
                IsWalking = false;
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
        IsWalking = true;
        RotateEnemy();
    }

    private void UseDoor()
    {
        if (nextPoint != null)
        {
            moveDir = nextPoint.position - transform.position;
            rb.velocity = moveDir.normalized * openDoorSpeed;
        }
        IsWalking = true;
        RotateEnemy();
    }

    private void Call()
    {
        if(callPoint != null)
        {
            nextPoint = null;
            moveDir = callPoint.position - transform.position;
            rb.velocity = moveDir.normalized * runSpeed;
            IsWalking = true;
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
}
public enum EnemyState
{
    Idle,
    Patrol,
    Gotcha,
    UseDoor,
    Sleep,
    Call,
    UseVerticalDoor
}
