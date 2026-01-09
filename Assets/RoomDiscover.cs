using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDiscover : MonoBehaviour
{
    private List<Player> playerInRoom = new List<Player>();
    private List<MoveEnemy> enemiesInRoom = new List<MoveEnemy>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out MoveEnemy enemy))
        {
            if (!enemiesInRoom.Contains(enemy)) enemiesInRoom.Add(enemy);
        }


        if (collision.TryGetComponent(out Player player))
        {
            playerInRoom.Add(player);
        }

        CheckGotcha();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MoveEnemy enemy))
        {
            enemiesInRoom.Remove(enemy);
        }


        if (collision.TryGetComponent(out Player player))
        {
            playerInRoom.Remove(player);
        }
    }

    private void CheckGotcha()
    {
        if(playerInRoom.Count > 0 && enemiesInRoom.Count > 0)
        {
            
            enemiesInRoom[0].GotchaEnemy(playerInRoom[0].transform);
            playerInRoom[0].PlayerGotcha();
        }
    }
}
