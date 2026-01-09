using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOpenDoor : MonoBehaviour
{
    private MoveEnemy moveEnemy;
    private HorizontalDoor door;

    public event EventHandler OnOpenDoor;
    public event EventHandler OnCloseDoor;

    private void Awake()
    {
        moveEnemy = GetComponentInParent<MoveEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out door)) return;

        if (!door.isOpen)
        {
            OnOpenDoor?.Invoke(this, EventArgs.Empty);
            door.UseEnemy(moveEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out door)) return;
        door.CloseDoor();

        OnCloseDoor.Invoke(this, EventArgs.Empty);

        door = null;
    }
}
