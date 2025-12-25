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

        animator.Update(0f);
    }

    
    void Update()
    {
        animator.SetBool(AnimationString.isOpen, notWallDoor.isOpen);
    }

    public void OpenDone()
    {
        notWallDoor.OpenDoor();
    }
}
