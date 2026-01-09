using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLogic : MonoBehaviour, IInteract
{
    [SerializeField] private Transform selectedPlace;
    [SerializeField] private Transform roomDiscover;

    public void Interact(Player player)
    {
        if (player.currentPlayerState == PlayerState.ExitHide)
        {
            roomDiscover.gameObject.SetActive(true);
        }
        else
        {
            roomDiscover.gameObject.SetActive(false);
            selectedPlace.gameObject.SetActive(false);
            player.PlayerHide(this.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;

        player.SetInteractable(this);
        selectedPlace.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;

        player.ClearInteractable(this);
        selectedPlace.gameObject.SetActive(false);
    }
}
