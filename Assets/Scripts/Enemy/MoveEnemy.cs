using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float openDoorSpeed = 3f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float distanceGotcha = 1f;

    [SerializeField] private EnemyState currentState;
    [SerializeField] private Transform callPoint;
    [SerializeField] private Transform currentPointPatrol;
    [SerializeField] private Transform offCallPoint;
    [SerializeField] private Transform exitDoorPatrol;
    [SerializeField] private CallEnemy callEnemy;
    
    private Transform nextVetticalDoor;
    private Vector2 moveDir;
    private Transform nextPointPatrol;
    private Rigidbody2D rb;
    private float distanceToPoint = 0.1f;
    private bool CallState;
    private EnemyOpenDoor colliderDoor;
    private EnemyState lastState;
    private bool isDoorVertical;
    private Transform targetPlayer;
    private bool isDie;
    private Transform currentTrap;
    private bool isGotcha = false;
    public float maxTimerOffCall = 2f;
    private float timerOffCall;

    private float timeToIdle;
    private float maxTimerToIdle = 2f;

    public EnemyState CurrentState => currentState;

    public event EventHandler OnCallDiactive;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderDoor = GetComponentInChildren<EnemyOpenDoor>();
    }

    private void OnEnable()
    {
        colliderDoor.OnOpenDoor += MoveEnemy_OnOpenDoor;
        colliderDoor.OnCloseDoor += MoveEnemy_OnCloseDoor;
        if (callEnemy != null)
        {
            callEnemy.OnActiveEnemy += MoveEnemy_OnActiveEnemy;
            callEnemy.OnDiactiveCall += MoveEnemy_OnDiactiveCall;
        }
    }

    private void MoveEnemy_OnDiactiveCall(object sender, EventArgs e)
    {
        SetState(EnemyState.OffCall);
        CallState = false;
    }

    private void MoveEnemy_OnActiveEnemy(object sender, EventArgs e)
    {
        SetState(EnemyState.Call);
    }

    private void MoveEnemy_OnCloseDoor(object sender, EventArgs e)
    {
        if (CallState)
        {
            SetState(EnemyState.Call);
        }
        else
        {
            SetState(lastState);
        }
    }

    private void MoveEnemy_OnOpenDoor(object sender, EventArgs e)
    {
        lastState  = currentState;
        SetState(EnemyState.UseDoor);
    }

    private void FixedUpdate()
    {
        if (currentState == EnemyState.Die)
        {
            return;
        }
        if (!isDie)
        {
            if (currentState == EnemyState.Gotcha)
            {
                Gotcha();
                return;
            }
            switch (currentState)
            {
                case EnemyState.Idle:
                    Idle();
                    break;

                case EnemyState.Patrol:
                    Patrol();
                    break;

                case EnemyState.EnterVerticalDoor:
                    Stop();
                    break;

                case EnemyState.Sleep:
                    Stop();
                    break;

                case EnemyState.UseDoor:
                    UseDoor();
                    break;

                case EnemyState.Call:
                    Call();
                    break;
                case EnemyState.OffCall:
                    OffCall();
                    break;
                case EnemyState.OnTrap:
                    Stop();
                    break;
            }
        }
    }

    private void Idle()
    {
        currentPointPatrol = nextPointPatrol;
        rb.velocity = Vector2.zero;
        timeToIdle += Time.fixedDeltaTime;
        
        if(timeToIdle > maxTimerToIdle)
        {
            SetState(EnemyState.Patrol);
            timeToIdle = 0;
        }
    }

    private void Patrol()
    {
        if (CallState)
        {
            SetState(EnemyState.Call);
        }
        moveDir = currentPointPatrol.position - transform.position;
        rb.velocity = moveDir.normalized * MoveSpeed;

        if (Vector2.Distance(transform.position, currentPointPatrol.position) <= distanceToPoint)
        {
            rb.velocity = Vector2.zero;
            SetState(EnemyState.Idle);
        }
        RotateEnemy();
        if(isDoorVertical)
        {
            SetState(EnemyState.EnterVerticalDoor);
        }

    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    private void UseDoor()
    {
        if (!CallState)
        {
            moveDir = currentPointPatrol.position - transform.position;
            rb.velocity = moveDir.normalized * openDoorSpeed;
        }
        else
        {
            moveDir = callPoint.position - transform.position;
            rb.velocity = moveDir.normalized * openDoorSpeed;
        }
            RotateEnemy();
    }

    private void Call()
    {
        if(callPoint != null)
        {
            CallState = true;
            currentPointPatrol = offCallPoint;
            moveDir = callPoint.position - transform.position;
            rb.velocity = moveDir.normalized * runSpeed;
            RotateEnemy();
        }
        if(isDoorVertical)
        {
            SetState(EnemyState.EnterVerticalDoor);
        }
    }

    private void Gotcha()
    {
        Vector2 target = (targetPlayer.position - transform.position).normalized;
        if (Vector2.Distance(targetPlayer.position, transform.position) < distanceGotcha)
        {
            rb.velocity = Vector2.zero;

        }
        else
            rb.velocity = target * MoveSpeed;
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

    public void SetNextPointParametrs(Transform point, float timeIdle)
    {
        nextPointPatrol = point;
        maxTimerToIdle = timeIdle;
    }

    public void SetNextCallPoint(Transform point)
    {
        callPoint = point;
    }

    public EnemyState GetEnemyState()
    {
        return currentState;
    }

    public void EnterVerticalDoor()
    {
        transform.position = nextVetticalDoor.position;
        currentState = EnemyState.ExitVerticalDoor;
        IsDoorVertical(false);
    }

    public void ExitVerticalDoorCall()
    {
        if (isGotcha)
        {
            currentState = EnemyState.Gotcha;
        }
        else
        {
            currentState = lastState;
            moveDir = callPoint.position - transform.position;
            if (currentState == EnemyState.Patrol)
            {
                currentPointPatrol = exitDoorPatrol;
            }
        }
    }

    public void SetNextVerticalDoor(Transform point)
    {
        nextVetticalDoor = point;
    }

    public void IsDoorVertical(bool amount)
    {
        isDoorVertical = amount;
    }

    public void GotchaEnemy(Transform playerTransfrom)
    {
        if (currentState == EnemyState.Gotcha)
            return;
        isGotcha = true;
        targetPlayer = playerTransfrom;
        if (!isDie && currentState != EnemyState.ExitVerticalDoor)
        {
            Debug.Log("GOTCHA!");

            currentState = EnemyState.Gotcha;
        }
    }

    private void OffCall()
    {
        rb.velocity = Vector2.zero;
        timerOffCall += Time.deltaTime;

        if (timerOffCall > maxTimerOffCall)
        {
            
            CallState = false;
            OnCallDiactive?.Invoke(this, EventArgs.Empty);

            
            if (currentState != EnemyState.UseDoor)
            {
                SetState(EnemyState.Patrol);
                lastState = EnemyState.Patrol;
                timerOffCall = 0;
            }
        }
    }

    private void SetState(EnemyState newState)
    {
        if (currentState == EnemyState.Gotcha)
            return;

        currentState = newState;
    }

    public void Die()
    {
        gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
        Debug.Log("Die");
        currentState = EnemyState.Die;
        rb.velocity = Vector2.zero;
        isDie = true;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void OnTrap(Transform triggerTrapTransform)
    {
        lastState = currentState;
        currentState = EnemyState.OnTrap;
        currentTrap = triggerTrapTransform.parent;
    }

    public void OutTrap()
    {
        currentTrap.gameObject.SetActive(false);
        currentState = lastState;
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
    EnterVerticalDoor = 6,
    ExitVerticalDoor = 7,
    OffCall = 8,
    Die = 9,
    OnTrap = 10
}
