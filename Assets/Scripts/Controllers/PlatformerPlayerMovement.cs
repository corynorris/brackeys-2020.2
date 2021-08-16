using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerMovement : MonoBehaviour
{
    public PlatformerCharacterController controller;

    public float runSpeed = 0f;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool jump = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
