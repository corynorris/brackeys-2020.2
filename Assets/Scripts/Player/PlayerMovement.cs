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
    private bool resetMenuKey;

    private Vector2 forceModifier = Vector2.zero;

    private float StepCooldown;
    private float lastStep;

    [SerializeField]
    private AudioClip[] insideSteps;

    [SerializeField]
    private AudioClip[] outsideSteps;

    private LevelController levelController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }


    void Update()
    {
        if (Time.timeScale == 0) return;

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

                if (lastStep + StepCooldown < Time.time)
                {
                    AudioClip clip;
                    float volume;
                    float delay;
                    if (player.isInside)
                    {
                        clip = insideSteps[Random.Range(0, insideSteps.Length - 1)];
                        volume = 0.4f;
                        delay = 0.05f;
                    }
                    else
                    {
                        clip = outsideSteps[Random.Range(0, outsideSteps.Length - 1)];
                        volume = 0.7f;
                        delay = 0.05f;
                    }

                    StepCooldown = clip.length + delay;
                    lastStep = Time.time;
                    Utils.spawnAudio(gameObject, clip, volume);
                }

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


    }

    public void ModifyForce(Vector2 forceModifier)
    {
        this.forceModifier = forceModifier;
    }

    private void FixedUpdate()
    {
        if (enableMovement) {
            rb.MovePosition(rb.position + forceModifier + movement.normalized * moveSpeed * levelController.GetBootsMultiplier() * Time.fixedDeltaTime );
            this.forceModifier = Vector2.zero; 
        }
    }

}
