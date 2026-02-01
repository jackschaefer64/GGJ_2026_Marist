using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter; 
    public GameObject winTextObject;
    public GameObject Player;
    private int count;
    public TextMeshProUGUI countText;
    public Image Mask;


    Vector2 movementInput;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); count = 0;
        SetCountText();
        winTextObject.SetActive(false);


        Color c = Mask.color;
        c.a = 0f;
        Mask.color = c;


    }


    IEnumerator FadeInImage(Image img, float duration)
    {
        Color c = img.color;
        c.a = 0f;
        img.color = c;

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = t / duration;
            img.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        //fade in colors of mask
        img.color = new Color(c.r, c.g, c.b, 1f);
    }



    void SetCountText()
    {
        countText.text = "Mask Shards: " + count.ToString() + "/5";

        if (count >= 5)
        {
            winTextObject.SetActive(true);

            // Shows the end mask
            Color c = Mask.color;
            c.a = 1f;               //makes image fully visible
            Mask.color = c;
         
           StartCoroutine(FadeInImage(Mask, 1.5f)); // 1.5 seconds Fade in mask
           

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            count++;
            SetCountText();
            other.gameObject.SetActive(false);
        }
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
