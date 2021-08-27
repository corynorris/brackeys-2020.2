using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipPlayerController : MonoBehaviour
{
    private bool moveUp = false;
    private bool moveDown = false;
    private bool moveRight = false;
    private bool moveLeft = false;
    private float speed = 10f;

    private bool freeToMove = true;
    

    [SerializeField]
    private Rigidbody2D rb;
    private ShipStationController menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (freeToMove)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                moveUp = true;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                moveUp = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                moveDown = true;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                moveDown = false;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                moveRight = true;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                moveRight = false;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                moveLeft = true;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                moveLeft = false;
            }
        }
    }

    private void FixedUpdate()
    {
        updatePosition();
    }

    private void updatePosition()
    {
        float verticalSpeed = 0;
        float horizontalSpeed = 0;
        if (moveUp && !moveDown)
        {
            verticalSpeed = speed;
        }
        else if (moveDown && !moveUp)
        {
            verticalSpeed = -speed;
        }

        if (moveRight && !moveLeft)
        {
            horizontalSpeed = speed;
        }
        else if (moveLeft && !moveRight)
        {
            horizontalSpeed = -speed;
        }
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    public void openMenu()
    {
        freeToMove = false;
        stop();
        //menu.turnOnMenu();
    }

    public void closeMenu()
    {
        freeToMove = true;
        menu.turnOffMenu();
        menu = null;
    }

    private void stop()
    {
        moveUp = false;
        moveDown = false;
        moveRight = false;
        moveLeft = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Station")
        {
            this.menu = collision.gameObject.GetComponentInParent<ShipStationController>();
            openMenu();
        }
    }
}
