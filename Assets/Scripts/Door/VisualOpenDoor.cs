using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class VisualOpenDoor : MonoBehaviour
{
    private HorizontalDoor notWallDoor;
    private Animator animator;

    private void Awake()
    {
        notWallDoor = GetComponentInParent<HorizontalDoor>();
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
