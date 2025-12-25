using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    private Animator animator;
    private MoveEnemy moveEnemy;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        moveEnemy = GetComponentInParent<MoveEnemy>();
        animator.SetBool(AnimationString.isWalking, moveEnemy.IsWalking);
    }

    private void Update()
    {
        switch (moveEnemy.GetEnemyState())
        {
            case EnemyState.Patrol:
                animator.SetBool(AnimationString.isWalking, moveEnemy.IsWalking);
                break;
            case EnemyState.Idle:
                animator.SetBool(AnimationString.isWalking, moveEnemy.IsWalking);
                break;
        }
    }
}
