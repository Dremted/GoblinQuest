using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Game_Manager game_manager;
    private Animator animator;
    private MoveEnemy enemy;

    private static readonly int StateHash = Animator.StringToHash("State");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponentInParent<MoveEnemy>();
    }

    private void Update()
    {
        animator.SetInteger(StateHash, (int)enemy.CurrentState);
    }

    public void EnterVerticalDoor()
    {
        enemy.EnterVerticalDoor();
    }

    public void ExitVerticalDoorCall()
    {
        enemy.ExitVerticalDoorCall();
    }

    public void OutTrap()
    {
        enemy.OutTrap();
    }

    public void EndAnimGame()
    {
        game_manager.CompleteGame();
    }
}

