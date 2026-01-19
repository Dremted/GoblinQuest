using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    public enum TrapType
    {
        Killer,
        Episodic
    }

    [SerializeField] private TrapType trapType;
    [SerializeField] private Transform itemNextTrap;

    public event EventHandler OnNextTrap;
    public event EventHandler OnActiveTrap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MoveEnemy enemy)) return;
        Debug.Log($"Enemy State: {enemy.GetEnemyState()}");
        if (enemy.GetEnemyState() != EnemyState.Gotcha)
        {
            OnActiveTrap?.Invoke(this, EventArgs.Empty);
            if (trapType == TrapType.Killer)
            {
                enemy.Die();
            }
            else
            {
                OnNextTrap?.Invoke(this, EventArgs.Empty);
                enemy.OnTrap(this.transform);
                
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MoveEnemy enemy)) return;
        if (itemNextTrap != null )
            itemNextTrap.gameObject.SetActive(true);
    }
}
