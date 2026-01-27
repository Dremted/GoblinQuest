using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CallEnemy : MonoBehaviour, IInteract
{
    private enum State
    {
        Active,
        Inactive
    }

    [SerializeField] MoveEnemy enemy;

    [Header("Waypoints")]
    [SerializeField] Transform callPoints;

    [Header("Visual state")]
    [SerializeField] Transform selected;
    [SerializeField] Transform active_UP;
    [SerializeField] Transform inactive_DOWN;


    private State currentState;

    public event EventHandler OnActiveEnemy;
    public event EventHandler OnDiactiveCall;

    // Subscribe to enemy event that notifies when the call should be deactivated
    private void OnEnable()
    {
        enemy.OnCallDiactive += Call_OnCallDiactive;
    }

    private void OnDisable()
    {
        enemy.OnCallDiactive -= Call_OnCallDiactive;
    }

    private void Call_OnCallDiactive(object sender, EventArgs e)
    {
        deactiveCall();
    }

    private void Start()
    {
        currentState = State.Inactive;
    }

    // Called by the player when interacting with the bell.
    // Activates the call only if it is currently inactive.
    public void Interact(Player player)
    {
        if(currentState == State.Inactive)
            Call();
        currentState = State.Active;
    }

    // Activates the call: enables enemy waypoints and updates visual state
    private void Call()
    {
        callPoints.gameObject.SetActive(true);
        OnActiveEnemy?.Invoke(this, EventArgs.Empty);

        selected.gameObject.SetActive(false);
        active_UP.gameObject.SetActive(true);
        inactive_DOWN.gameObject.SetActive(false);
    }

    //Call shutdown is performed by the enemy
    public void deactiveCall()
    {
        callPoints.gameObject.SetActive(false);
        currentState = State.Inactive;

        active_UP.gameObject.SetActive(false);
        inactive_DOWN.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //The enemy reached the bell and disconnected it.
        if (collision.gameObject.TryGetComponent(out MoveEnemy enemy))
        {
            OnDiactiveCall?.Invoke(this, EventArgs.Empty);
        }
        // Player entered interaction zone
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        {
            player.SetInteractable(this);
            if(currentState == State.Inactive)
                selected.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        {
            player.ClearInteractable(this);
            selected.gameObject.SetActive(false);
        }
    }
}
