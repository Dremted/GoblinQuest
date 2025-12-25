using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotWallDoor : MonoBehaviour, IInteract
{
    [SerializeField] Transform selected;
    [SerializeField] Transform colDoor;

    private Player currentPlayer;
    private MoveEnemy enemy;
    private StateDoor currentStateDoor;


    public bool isOpen => currentStateDoor == StateDoor.Open;

    private Collider2D col;
    

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        currentStateDoor = StateDoor.Close;
    }


    public void Interact(Player player)
    {
        if (!isOpen)
        {
            currentStateDoor = StateDoor.Open;
            currentPlayer = player;
            currentPlayer.SetPlayerState(PlayerState.OpenDoor);
            if (col.enabled)
            {
                PositionPlayer();
                colDoor.gameObject.SetActive(false);
                selected.gameObject.SetActive(isOpen);
            }
        }
    }

    public void SetHighlighted(bool value)
    {
        selected.gameObject.SetActive(value);   
    }

    public void OpenDoor()
    {
        
        if (currentPlayer != null)
        {

            currentPlayer.SetPlayerState(PlayerState.Idle);
            currentPlayer = null;
        }
        if (enemy != null)
        {
            enemy = null;
        }
        currentStateDoor = StateDoor.Open;
    }

    public void PositionPlayer()
    {
        if (currentPlayer != null)
        {
            Vector3 positionPlayer = currentPlayer.transform.position - transform.position;

            if (positionPlayer.x > 0)
            {

            }
            else
            {

            }
        }
    }

    public void UseEnemy(MoveEnemy moveEnemy)
    {
        enemy = moveEnemy;
        enemy.SetEnemyState(EnemyState.UseDoor);

        Vector3 positionEnemy = enemy.transform.position - transform.position;
        if (positionEnemy.x > 0)
        {

        }
        else
        {

        }
        currentStateDoor = StateDoor.Use;
        colDoor.gameObject.SetActive(false);
    }

    public void CloseDoor(MoveEnemy moveEnemy)
    {
        if(enemy == null)
        {
            enemy = moveEnemy;
        }
        Vector3 positionEnemy = enemy.transform.position - transform.position;
        if (positionEnemy.x > 0)
        {

        }
        else
        {

        }
        colDoor.gameObject.SetActive(true);
    }
}

public enum StateDoor
{
    Open,
    Close,
    Use
}
