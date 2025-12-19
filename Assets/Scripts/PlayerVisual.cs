using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private Player player;
    private PlayerState lastState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        animator.SetBool(AnimationString.isWalking, player.IsWalking);

        if (player.currentPlayerState == lastState) return;

        switch (player.currentPlayerState)
        {
            case PlayerState.EnterDoor:
                animator.SetTrigger(AnimationString.EnterDoor);
                break;

            case PlayerState.ExitDoor:
                animator.SetTrigger(AnimationString.ExitDoor);
                break;
        }

        lastState = player.currentPlayerState;
    }

    public void OnDoorEntered()
    {
        Debug.Log("PlayerVisual chaek");
        player.OnDoorEntered();
    }

    public void OnDoorExitFinished()
    {
        player.OnDoorExitFinished();
    }
}
