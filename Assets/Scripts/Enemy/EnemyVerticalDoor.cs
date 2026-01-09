using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVerticalDoor : MonoBehaviour
{
    [Header("Call Mode Points")]
    [SerializeField] private Transform nextVerticalDoor;    // Transfer point
    [SerializeField] private Transform callPoint;           // Point of movement after exiting the door

    [Header("State active transfer")]
    [SerializeField] public EnemyState activeState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent<MoveEnemy>(out MoveEnemy enemy)) return;

        if (enemy.GetEnemyState() == activeState)
        {
            enemy.SetNextVerticalDoor(nextVerticalDoor);
            enemy.IsDoorVertical(true);
            enemy.SetNextCallPoint(callPoint);
        }
    }
}
