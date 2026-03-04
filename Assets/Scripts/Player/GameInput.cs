using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    public event EventHandler OnMenuAction;
    public event EventHandler OnTutorial;
    public event EventHandler OnShiftActive;

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
        playerAction.Player.Tutorial.Enable();

        playerAction.Player.Move.performed += SetInputMove;
        playerAction.Player.Move.canceled += SetInputMove;

        playerAction.Player.Interaction.performed += Interact_Performed;
        playerAction.Player.Menu.performed += Menu_Performed;
        playerAction.Player.Tutorial.performed += Tutorial_Performed;
        playerAction.Player.CameraMove.performed += CameraMove_performed;
        playerAction.Player.CameraMove.canceled += CameraMove_performed;
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
        playerAction.Player.Menu.performed -= Menu_Performed;
        playerAction.Player.Tutorial.performed -= Tutorial_Performed;
        playerAction.Player.CameraMove.performed -= CameraMove_performed;
    }

    private void CameraMove_performed(InputAction.CallbackContext context)
    {
        OnShiftActive?.Invoke(this, EventArgs.Empty);
    }

    private void Tutorial_Performed(InputAction.CallbackContext context)
    {
        OnTutorial?.Invoke(this, EventArgs.Empty);
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
