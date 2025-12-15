using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameInput gameInput;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 3f;

    
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public bool IsWalking { get; private set;}
    public bool Ladder = false;

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection.normalized * moveSpeed;
    }

    private void PlayerMove()
    {
        moveDirection = gameInput.GetInputMove();

        if(!Ladder)
        {
            moveDirection.y = 0;
        }

        IsWalking = moveDirection != Vector2.zero;
        RotatePlayer();
    }

    private void RotatePlayer()
    {

        if (moveDirection.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveDirection.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

}
