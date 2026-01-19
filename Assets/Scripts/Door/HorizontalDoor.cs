using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDoor : MonoBehaviour, IInteract
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
            selected.gameObject.SetActive(false);
            if (col.enabled)
            {
                colDoor.gameObject.SetActive(false);
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
        currentStateDoor = StateDoor.Open;
    }

    public void UseEnemy(MoveEnemy moveEnemy)
    {
        enemy = moveEnemy;

        currentStateDoor = StateDoor.Open;
        colDoor.gameObject.SetActive(false);
    }

    public void CloseDoor()
    {
        currentStateDoor = StateDoor.Close;
        colDoor.gameObject.SetActive(true);
        enemy = null;
    }
}

public enum StateDoor
{
    Open,
    Close,
    Use
}
