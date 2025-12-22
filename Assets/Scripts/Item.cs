using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteract
{
    [SerializeField] private ItemsSO itemsSO;
    [SerializeField] private Transform selectedItem;

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
            selectedItem.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        {
            player.ClearInteractable(this);
            selectedItem.gameObject.SetActive(false);
        }
    }
}
