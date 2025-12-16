using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;

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


        playerAction.Player.Move.performed += SetInputMove;
        playerAction.Player.Move.canceled += SetInputMove;

        playerAction.Player.Interaction.performed += Interact_Performed;
    }

    private void OnDisable()
    {
        playerAction.Player.Move.Disable();
        playerAction.Player.Interaction.Disable();

        playerAction.Player.Move.performed -= SetInputMove;
        playerAction.Player.Move.canceled -= SetInputMove;

        playerAction.Player.Interaction.performed -= Interact_Performed;
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

}
