using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotWallDoor : MonoBehaviour, IInteract
{
    [SerializeField] Transform selected;

    private Player currentPlayer;
    private MoveEnemy enemy;

    private Collider2D col;

    public bool isOpenLeft {  get; private set; }
    public bool isOpenRight { get; private set; }

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void Interact(Player player)
    {
        currentPlayer = player;
        currentPlayer.SetPlayerState(PlayerState.OpenDoor);
        if (col.enabled)
        {
            PositionPlayer();
            col.enabled = false;
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
            enemy.SetEnemyState(EnemyState.Patrol);
            enemy = null;
        }

    }

    public void PositionPlayer()
    {
        if (currentPlayer != null)
        {
            Vector3 positionPlayer = currentPlayer.transform.position - transform.position;

            if (positionPlayer.x > 0)
            {
                isOpenLeft = true;
            }
            else
            {
                isOpenRight = true;
            }
        }
    }

    public void UseEnemy(MoveEnemy moveEnemy)
    {
        enemy = moveEnemy;
        enemy.SetEnemyState(EnemyState.UseDoor);
        if (!isOpenLeft || !isOpenRight)
        {
            Vector3 positionEnemy = enemy.transform.position - transform.position;
            if (positionEnemy.x > 0)
            {
                isOpenLeft = true;
            }
            else
            {
                isOpenRight = true;
            }
        }
    }
}
