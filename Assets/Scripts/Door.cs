using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private LayerMask playerLayerMask;
    
    [SerializeField] private Transform nextDoor;
    public Transform NextDoor => nextDoor;
    private Player player;
    private EnemyState enemyState;
    public bool isOpen {  get; private set ; }

    public void Interact(Player player)
    {
        player.EnterDoor(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            player = collision.gameObject.GetComponent<Player>();
            if (player == null) return;
            
            player.SetInteractable(this);
        }
        if(!collision.TryGetComponent<MoveEnemy>(out MoveEnemy enemy)) return;

        enemyState = enemy.GetEnemyState();
        if(enemyState == EnemyState.Call)
        {
            enemy.SetEnemyState(EnemyState.UseVerticalDoor);
            enemy.transform.position = nextDoor.position;
 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.ClearInteractable(this);

        if (!collision.TryGetComponent<MoveEnemy>(out MoveEnemy enemy)) return;
    }
    
    public void SetFalg(bool value)
    {
        isOpen = value;
    }
}

