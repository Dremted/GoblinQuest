using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotWallDoor : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Player>(out Player player)) return;
        
        player.SetPlayerState(PlayerState.OpenDoor);
    }
}
