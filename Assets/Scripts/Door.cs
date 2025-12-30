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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.ClearInteractable(this);

    }
    
    public void SetFlag(bool value)
    {
        isOpen = value;
    }
}

