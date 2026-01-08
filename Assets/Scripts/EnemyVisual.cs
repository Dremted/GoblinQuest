using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
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
}

