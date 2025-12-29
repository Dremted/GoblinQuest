using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVerticalDoor : MonoBehaviour
{
    [SerializeField] private Transform nextVerticalDoor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent<MoveEnemy>(out MoveEnemy enemy)) return;

        if (enemy.GetEnemyState() == EnemyState.Call)
        {

            enemy.SetNextVerticalDoor(nextVerticalDoor);
            enemy.SetEnemyState(EnemyState.UseVerticalDoor);
            
        }
    }
}
