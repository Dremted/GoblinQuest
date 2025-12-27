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
    [SerializeField] Transform selected;
    [SerializeField] GameObject pointPatrol;
    private State currentState;

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
        enemy.SetEnemyState(EnemyState.Call);
        pointPatrol.SetActive(false);
        selected.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
