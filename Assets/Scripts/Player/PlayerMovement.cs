using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enableMovement = true;
    private bool facingRight = false;

    public float moveSpeed = 1.0f;
    
    private Player player;
    private Animator body;

    private Vector2 forceModifier = Vector2.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
  
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        bool canMove = player.CanMove(); 

        if (canMove)
        {
            enableMovement = true;
            float currentSpeed = movement.SqrMagnitude();
            player.SetSpeed(currentSpeed);

    
   
            if (currentSpeed > 0.01f)
            {
                player.SetForwardDirection(movement);
       

                if (!facingRight && movement.x > 0.5f)
                {
                    player.transform.Rotate(0, 180, 0);
                    facingRight = true;
                }

                if (facingRight && movement.x < 0.5f)
                {
                    player.transform.Rotate(0, 180, 0);
                    facingRight = false;
                }
            }
        }
        else
        {
            enableMovement = false;
        }

        if (player.menuAvaliable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.openMenu();
            }
        }

        if (player.menuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                player.closeMenu();
            }
        }

    }

    public void ModifyForce(Vector2 forceModifier)
    {
        this.forceModifier = forceModifier;
    }

    private void FixedUpdate()
    {
        if (enableMovement) {
            rb.MovePosition(rb.position + forceModifier + movement * moveSpeed * Time.fixedDeltaTime );
            this.forceModifier = Vector2.zero; 
        }
    }

}
