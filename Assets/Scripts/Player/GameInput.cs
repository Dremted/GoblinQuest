using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    public event EventHandler OnMenuAction;

    private Player_Action playerAction;
    private Vector2 inputAction;

    private void Awake()
    {
        playerAction = new Player_Action();
    }

    private void OnEnable()
    {
        playerAction.Player.Move.Enable();
        playerAction.Player.Interaction.Enable();
        playerAction.Player.CameraMove.Enable();
        playerAction.Player.Menu.Enable();

        playerAction.Player.Move.performed += SetInputMove;
        playerAction.Player.Move.canceled += SetInputMove;

        playerAction.Player.Interaction.performed += Interact_Performed;
        playerAction.Player.Menu.performed += Menu_Performed;
    }



    private void OnDisable()
    {
        playerAction.Player.Move.Disable();
        playerAction.Player.Interaction.Disable();
        playerAction.Player.CameraMove.Disable();
        playerAction.Player.Menu.Disable();

        playerAction.Player.Move.performed -= SetInputMove;
        playerAction.Player.Move.canceled -= SetInputMove;

        playerAction.Player.Interaction.performed -= Interact_Performed;
    }
    private void Menu_Performed(InputAction.CallbackContext context)
    {
        OnMenuAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void SetInputMove(InputAction.CallbackContext context)
    {
        inputAction = context.ReadValue<Vector2>();
    }

    public Vector2 GetInputMove()
    {
        return inputAction;
    }

    public bool CameraMode()
    {
        return playerAction.Player.CameraMove.IsPressed();
    }
}
