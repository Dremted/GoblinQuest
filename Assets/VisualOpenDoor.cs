using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class VisualOpenDoor : MonoBehaviour
{
    private NotWallDoor notWallDoor;
    private Animator animator;

    private void Awake()
    {
        notWallDoor = GetComponentInParent<NotWallDoor>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        animator.SetBool(AnimationString.isOpenLeft, notWallDoor.isOpenLeft);
        animator.SetBool(AnimationString.isOpenRight, notWallDoor.isOpenRight);
    }

    public void OpenDone()
    {
        notWallDoor.OpenDoor();
    }
}
