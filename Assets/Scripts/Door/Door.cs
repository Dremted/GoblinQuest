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

    //The player enters the "Enter Door" state
    public void Interact(Player player)
    {
        player.EnterDoor(this);
    }

    //Sets doors to interactive objects for the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            player = collision.gameObject.GetComponent<Player>();
            if (player == null) return;
            
            player.SetInteractable(this);
        }
    }
    //Dlete doors to interactive objects for the player
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.ClearInteractable(this);

    }
}

