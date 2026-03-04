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
    [SerializeField] private Game_Manager gameManager;

    [SerializeField] private Transform thisIdlePos;
    [SerializeField] private Transform anotherIdlePos;
    [SerializeField] private PointPatrol pointToAnotherStart;

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
                if (anotherIdlePos != null)
                {
                    thisIdlePos.gameObject.SetActive(false);
                    anotherIdlePos.gameObject.SetActive(true);
                    pointToAnotherStart.SetNewPoint(anotherIdlePos);
                }
            }
            else
            {
                OnNextTrap?.Invoke(this, EventArgs.Empty);
                enemy.OnTrap(this.transform);
                if (anotherIdlePos != null)
                {
                    thisIdlePos.gameObject.SetActive(false);
                    anotherIdlePos.gameObject.SetActive(true);
                    pointToAnotherStart.SetNewPoint(anotherIdlePos);
                }
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
