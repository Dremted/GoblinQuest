using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPoint : MonoBehaviour
{
    [SerializeField] Transform nextCallPont;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<MoveEnemy>(out MoveEnemy enemy)) return;

        if (nextCallPont != null)
            enemy.SetEnemyState(EnemyState.Call);
            enemy.SetNextCallPoint(nextCallPont);
    }
}
