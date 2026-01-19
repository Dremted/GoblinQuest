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
        animator.SetBool(AnimationString.SetTrap, player.IsSetTrap);


        if (player.currentPlayerState == lastState) return;


        switch (player.currentPlayerState)
        {
            case PlayerState.EnterDoor:
                animator.SetTrigger(AnimationString.EnterDoor);
                break;

            case PlayerState.ExitDoor:
                animator.SetTrigger(AnimationString.ExitDoor);
                break;
            case PlayerState.EnterHide:
                animator.SetTrigger(AnimationString.EnterHide);
                break;
            case PlayerState.ExitHide:
                animator.SetTrigger(AnimationString.ExitHide);
                break;
            case PlayerState.GetItem:
                animator.SetTrigger(AnimationString.GetItem);
                break;
        }

        lastState = player.currentPlayerState;
    }

    public void OnDoorEntered()
    {
        player.OnDoorEntered();
    }

    public void OnDoorExitFinished()
    {
        player.OnDoorExitFinished();
    }

    public void OnItemGeting()
    {
        player.ItemGetting();
    }

    public void OnHideExit()
    {
        player.PlayerExitHide();
    }
}
