using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPatrol : MonoBehaviour
{
    [SerializeField] private Transform nextPoint;
    [SerializeField] private float idleTime;
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private Transform objectUseOnItem;

    public void SetNewPoint(Transform newPoint)
    {
        this.nextPoint = newPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MoveEnemy moveEnemy)) return;
        moveEnemy.SetIdleState(enemyState);
        moveEnemy.SetNextPointParametrs(nextPoint, idleTime);
        if(objectUseOnItem != null)
            objectUseOnItem.gameObject.SetActive(false);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectUseOnItem != null)
            objectUseOnItem.gameObject.SetActive(true);
    }
}

