using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameInput gameInput;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 3f;

    
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public bool IsWalking { get; private set;}

    private IInteract currentInteract;
    public PlayerState currentPlayerState { get; private set; }
    private Door currentDoor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPlayerState = PlayerState.Idle;
    }

    private void OnEnable()
    {
        gameInput.OnInteract += GameInput_Interact;
    }

    private void GameInput_Interact(object sender, EventArgs e)
    {
        if (currentPlayerState != PlayerState.Idle && currentPlayerState != PlayerState.Move)
            return;

        currentInteract?.Interact(this);
    }

    private void Update()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        if (currentPlayerState == PlayerState.Move)
            rb.velocity = moveDirection.normalized * moveSpeed;
        else
            rb.velocity = Vector2.zero;
    }

    private void PlayerMove()
    {
        moveDirection = gameInput.GetInputMove();


        if(moveDirection != Vector2.zero)
        {
            currentPlayerState = PlayerState.Move;
            IsWalking = true;
        }
        else
        {
            currentPlayerState = PlayerState.Idle;
            IsWalking = false;
        }

            RotatePlayer();
    }

    private void RotatePlayer()
    {

        if (moveDirection.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveDirection.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void UpdateState()
    {
        switch (currentPlayerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                PlayerMove();
                break;
            case PlayerState.EnterDoor:
            case PlayerState.ExitDoor:
                break;
        }
    }

    public void SetInteractable(IInteract interact)
    {
        currentInteract = interact;
    }

    public void ClearInteractable(IInteract interact)
    {
        if (currentInteract == interact)
            currentInteract = null;
    }

    public void EnterDoor(Door door)
    {
        if (currentPlayerState != PlayerState.Idle && currentPlayerState != PlayerState.Move)
            return;

        currentDoor = door;
        currentPlayerState = PlayerState.EnterDoor;
        IsWalking = false;
    }

    public void OnDoorEntered()
    {
        if (currentDoor == null) return;

        transform.position = currentDoor.NextDoor.position;
        currentPlayerState = PlayerState.ExitDoor;
    }

    public void OnDoorExitFinished()
    {
        currentDoor = null;
        currentPlayerState = PlayerState.Idle;
    }
}
public enum PlayerState
{
    Idle,
    Move,
    EnterDoor,
    ExitDoor
}
