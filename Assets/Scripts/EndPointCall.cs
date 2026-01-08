using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointCall : MonoBehaviour
{
    [SerializeField] private Transform pointToPatrol;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent<MoveEnemy>(out MoveEnemy enemy)) return;
        //enemy.SetNextPoint(pointToPatrol);
        if(enemy.GetEnemyState() == EnemyState.Gotcha) return;

        //enemy.SetEnemyState(EnemyState.OffCall);
        
    }
}
