using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotWallDoor : MonoBehaviour, IInteract
{
    [SerializeField] Transform selected;

    private Player currentPlayer;

    private Collider2D col;

    public bool isOpen {  get; private set; }

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
            col.enabled = false;
            isOpen = true;
        }
    }

    public void SetHighlighted(bool value)
    {
        selected.gameObject.SetActive(value);
    }

    public void OpenDoor()
    {
        currentPlayer.SetPlayerState(PlayerState.Idle);
        currentPlayer = null;
    }
}
