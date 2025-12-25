using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOpenDoor : MonoBehaviour
{
    private MoveEnemy moveEnemy;
    private NotWallDoor door;

    private void Awake()
    {
        moveEnemy = GetComponentInParent<MoveEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out door)) return;
        if (!door.isOpen)
        {
            door.UseEnemy(moveEnemy);
        }
        else
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out door)) return;

        door = null;
    }
}
