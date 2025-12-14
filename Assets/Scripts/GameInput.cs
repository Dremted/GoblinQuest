using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private Player_Action playerAction;
    private Vector2 inputAction;

    private void Awake()
    {
        playerAction = new Player_Action();
        playerAction.Player.Move.Enable();

    }

    private void OnEnable()
    {
        playerAction.Player.Move.performed += SetInputMove;
        playerAction.Player.Move.canceled += SetInputMove;
    }

    private void OnDisable()
    {
        playerAction.Player.Move.performed -= SetInputMove;
        playerAction.Player.Move.canceled -= SetInputMove;
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
