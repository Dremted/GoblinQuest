using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOpenDoor : MonoBehaviour
{
    private MoveEnemy moveEnemy;
    private HorizontalDoor door;
    private EnemyState enemyState;

    private void Awake()
    {
        moveEnemy = GetComponentInParent<MoveEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out door)) return;

        enemyState = moveEnemy.GetEnemyState();
        if (!door.isOpen)
        {
            door.UseEnemy(moveEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out door)) return;
            door.CloseDoor();
        
        moveEnemy.SetEnemyState(enemyState);

        door = null;
    }
}
