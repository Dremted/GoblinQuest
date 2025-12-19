using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteract
{
    [SerializeField] private ItemsSO itemsSO;


    public void Interact(Player player)
    {
        player.AddItemInventory(itemsSO);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        {
            player.SetInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        {
            player.ClearInteractable(this);
        }
    }
}
