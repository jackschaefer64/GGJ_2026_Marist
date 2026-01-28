using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
            
    Vector2 movementInput;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        
        if(movementInput != Vector2.zero) //If movement is not zero, try to move the player
        {
           bool flag = TryToMove(movementInput);
           //Bool that stores the return value from TryToMove
           if(!flag)
            {
                //If normal movement doesn't work, try only X axis
                flag = TryToMove(new Vector2(movementInput.x, 0));
                if(!flag)
                {
                    //If X axis doesn't work, try only Y axis
                    flag = TryToMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", flag);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
        //Set direction of sprite to movement direction
        if(movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
            

    }

    private bool TryToMove(Vector2 direction)
    {
        //Check for potential collisions
        int count = rb.Cast(
            direction, //X and Y values that represent the direction from the body
            movementFilter, //Determines where a collision can occur
            castCollisions, //A list of collisions stored after the cast
            moveSpeed * Time.fixedDeltaTime + collisionOffset //The amount to cast 
            );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

    }
}
