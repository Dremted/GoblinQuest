using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCol : MonoBehaviour
{
    public event EventHandler OnActiveGameOver;

    [SerializeField]private GameObject playerVisual;
    [SerializeField] private GameObject Smoke;

    private void OnEnable()
    {
        Smoke.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.TryGetComponent(out MoveEnemy enemy)) return;

        OnActiveGameOver?.Invoke(this, EventArgs.Empty);
        playerVisual.SetActive(false);
        enemy.gameObject.SetActive(false);
        Smoke.SetActive(true);
    }
}
