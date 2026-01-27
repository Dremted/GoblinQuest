using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clue : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject visual_Clue;
    [SerializeField] private GameObject message_Clue;

    public void Interact(Player player)
    {
        if(player.currentPlayerState != PlayerState.Move)
            message_Clue.SetActive(true);
    }

    private void Awake()
    {
        visual_Clue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;

        player.SetInteractable(this);

        visual_Clue.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;

        player.ClearInteractable(this);

        visual_Clue.SetActive(false);
        message_Clue.SetActive(false);
    }
}
