using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoorVisual : MonoBehaviour
{
    private Animator animator;
    private Door door;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        door = GetComponentInParent<Door>();
    }
}
