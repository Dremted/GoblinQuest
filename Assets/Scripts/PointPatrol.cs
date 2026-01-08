using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPatrol : MonoBehaviour
{
    [SerializeField] private Transform nextPoint;
    [SerializeField] private float idleTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MoveEnemy moveEnemy)) return;

        moveEnemy.SetNextPointParametrs(nextPoint, idleTime);
    }
}

