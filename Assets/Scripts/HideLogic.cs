using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles logic for hiding places where the player can take cover
public class HideLogic : MonoBehaviour, IInteract
{
    [SerializeField] private Transform selectedPlace;  // Visual effect
    [SerializeField] private Transform roomDiscover;    
    [SerializeField] private Player player;


    // When the player leaves the shelter, the capture rooms are activated
    private void OnEnable() 
    {
        player.OnActiveRoom += HideLogic_OnActiveRoom;
    }
    private void OnDisable()
    {
        player.OnActiveRoom -= HideLogic_OnActiveRoom;
    }

    // Enabling capture rooms
    private void HideLogic_OnActiveRoom(object sender, EventArgs e)
    {
        roomDiscover.gameObject.SetActive(true);
    }

    // Hides the shelter visuals and moves the player into the hiding place
    public void Interact(Player player)
    {
            roomDiscover.gameObject.SetActive(false);
            selectedPlace.gameObject.SetActive(false);
            player.PlayerHide(this.transform);
    }

    // Passes an item for activation to the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;

        player.SetInteractable(this);
        selectedPlace.gameObject.SetActive(true);
    }

    // Removes the active item. Disables visual selection.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;

        player.ClearInteractable(this);
        selectedPlace.gameObject.SetActive(false);
    }
}
