using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameInput gameInput;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float distanceRay = 1f;
    [SerializeField] private float speedCamera = 12f;
    [SerializeField] private LayerMask doorLayerMask;
    [SerializeField] private Transform cameraAnchor;
    [SerializeField] private Transform colDiscover;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    public bool IsWalking { get; private set;}
    public bool IsSetTrap {  get; private set;}

    private IInteract currentInteract;
    private IInteract triggerInteract;
    public PlayerState currentPlayerState { get; private set; }

    private Door currentDoor;

    [SerializeField] private Inventory inventory;

    public event EventHandler OnActiveRoom;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPlayerState = PlayerState.Idle;
        
    }

    private void OnEnable()
    {
        gameInput.OnInteract += GameInput_Interact;
    }

    private void OnDisable()
    {
        gameInput.OnInteract -= GameInput_Interact;
    }

    private void GameInput_Interact(object sender, EventArgs e)
    {
        if (currentPlayerState != PlayerState.Idle && currentPlayerState != PlayerState.Move && currentPlayerState != PlayerState.EnterHide)
            return;

        if(currentPlayerState == PlayerState.EnterHide)
        {
            currentPlayerState = PlayerState.ExitHide;
        }
        else
            currentInteract?.Interact(this);
    }

    private void Update()
    {
        UpdateState();

        DetectInteractable();
    }

    private void FixedUpdate()
    {
        rb.velocity = currentPlayerState == PlayerState.Move
            ?moveDirection.normalized * moveSpeed 
            : rb.velocity = Vector2.zero;
            
    }

    private void PlayerMove()
    {
        if (gameInput.CameraMode())
        {
            MoveCameraAnchor();

            UpdateMoveState();
            rb.velocity = Vector2.zero;
        }
        else
        {
            RemoveCamera();

            ReadInput();

            UpdateMoveState();

            RotatePlayer();
        }
    }

    private void RemoveCamera()
    {
        cameraAnchor.position = Vector3.Lerp(cameraAnchor.position, transform.position, Time.deltaTime * 5f);
    }

    private void MoveCameraAnchor()
    {
        Vector2 moveVector = gameInput.GetInputMove();
        Vector3 moveCamera = new Vector3(moveVector.x, moveVector.y, 0) * speedCamera * Time.deltaTime;

        cameraAnchor.Translate(moveCamera, Space.World);
    }

    private void ReadInput()
    {
        moveDirection = gameInput.GetInputMove();

        moveDirection.y = 0;
    }

    private void UpdateMoveState()
    {
        if (moveDirection != Vector2.zero)
        {
            if (currentPlayerState == PlayerState.SetTrap)
            {
                PlayerStopInteract();
            }
            currentPlayerState = PlayerState.Move;
            IsWalking = true;
        }
        else
        {
            if (currentPlayerState != PlayerState.SetTrap)
            {
                currentPlayerState = PlayerState.Idle;
                IsWalking = false;
            }
        }
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

    private void DetectInteractable()
    {
        Vector2 facingDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            facingDirection,
            distanceRay,
            doorLayerMask
        );

        if (hit.collider != null && hit.collider.TryGetComponent<IInteract>(out var rayInteract))
        {
            if (currentInteract != rayInteract)
            {
                ResetVisuals(); 
                currentInteract = rayInteract;
                if (currentInteract is HorizontalDoor door && !door.isOpen) door.SetHighlighted(true);
            }
        }
        else if(currentInteract != triggerInteract)
        {
            ResetVisuals();
            currentInteract = triggerInteract;
        }
    }

    private void ResetVisuals()
    {
        if (currentInteract is HorizontalDoor door )
        {
            door.SetHighlighted(false);
        }
    }


    private void UpdateState()
    {
        switch (currentPlayerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
            case PlayerState.SetTrap:
                PlayerMove();
                break;
            case PlayerState.EnterHide:
                if(gameInput.CameraMode())
                {
                    MoveCameraAnchor();
                }
                else
                {
                    RemoveCamera();
                }
                    break;
            case PlayerState.OpenDoor:
            case PlayerState.EnterDoor:
            case PlayerState.ExitDoor:
            case PlayerState.ExitHide:
            case PlayerState.Gotcha:
                break;
        }
    }

    public void SetInteractable(IInteract interact)
    {
        triggerInteract = interact;
    }

    public void ClearInteractable(IInteract interact)
    {
        if (triggerInteract == interact) triggerInteract = null;
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

    public void AddItemInventory(ItemsSO itemsSO)
    {
        inventory.AddInventory(itemsSO);
    }

    public void GetItem()
    {
        currentPlayerState = PlayerState.GetItem;
    }

    public void ItemGetting()
    {
        currentPlayerState = PlayerState.Idle;  
    }

    public void PlayerSetTrap()
    {
        currentPlayerState = PlayerState.SetTrap;
        IsSetTrap = true;
    }

    public void PlayerGotcha()
    {
        SetPlayerState(PlayerState.Gotcha);
        colDiscover.gameObject.SetActive(true);
    }

    public void PlayerHide(Transform placeTransform)
    {
        currentPlayerState = PlayerState.EnterHide;
        transform.position = placeTransform.position;
    }

    public void PlayerExitHide()
    {
        currentPlayerState = PlayerState.Idle;
        OnActiveRoom?.Invoke(this, EventArgs.Empty);
    }

    public void PlayerStopInteract()
    {
        currentPlayerState = PlayerState.Idle;
        IsSetTrap = false;
    }

    public void SetPlayerState(PlayerState playerState)
    {
        currentPlayerState = playerState;

    }
}
public enum PlayerState
{
    Idle,
    Move,
    EnterDoor,
    ExitDoor,
    SetTrap,
    OpenDoor,
    EnterHide,
    ExitHide,
    Gotcha,
    GetItem
}
