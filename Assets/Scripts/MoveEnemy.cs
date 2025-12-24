using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float distanceRay=2f;
    [SerializeField] private LayerMask doorLayerMask;

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

    private void Update()
    {
        Vector2 facingDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            facingDirection,
            distanceRay,
            doorLayerMask
            );
        if (hit.collider != null && hit.collider.TryGetComponent<NotWallDoor>(out NotWallDoor notWallDoor))
        {
            notWallDoor.UseEnemy(this);
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
            case EnemyState.UseDoor:
                rb.velocity = Vector3.zero;
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
