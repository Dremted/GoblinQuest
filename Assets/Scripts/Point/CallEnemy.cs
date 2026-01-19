using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEnemy : MonoBehaviour, IInteract
{
    private enum State
    {
        Active,
        Diactive
    }

    [SerializeField] MoveEnemy enemy;

    [SerializeField] GameObject pointCall;

    [SerializeField] Transform selected;
    [SerializeField] Transform active_UP;
    [SerializeField] Transform diactive_DOWN;
    [SerializeField] Transform pointBackBase;
    [SerializeField] Transform callPoints;

    private State currentState;

    public event EventHandler OnActiveEnemy;
    public event EventHandler OnDiactiveCall;

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
        currentState = State.Diactive;
    }

    public void Interact(Player player)
    {
        if(currentState == State.Diactive)
            Call();
        currentState = State.Active;
    }

    private void Call()
    {
        callPoints.gameObject.SetActive(true);
        OnActiveEnemy?.Invoke(this, EventArgs.Empty);

        pointCall.SetActive(true);

        selected.gameObject.SetActive(false);
        active_UP.gameObject.SetActive(true);
        diactive_DOWN.gameObject.SetActive(false);
    }

    public void deactiveCall()
    {
        callPoints.gameObject.SetActive(false);
        currentState = State.Diactive;

        pointCall.SetActive(false);

        active_UP.gameObject.SetActive(false);
        diactive_DOWN.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out MoveEnemy enemy))
        {
            OnDiactiveCall?.Invoke(this, EventArgs.Empty);

        }
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        {
            player.SetInteractable(this);
            if(currentState == State.Diactive)
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
