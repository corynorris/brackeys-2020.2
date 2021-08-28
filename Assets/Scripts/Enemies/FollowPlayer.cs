using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(CircleCollider2D))]
public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 1f;
    public float moveSpeed = 1f;
    public float closestDistance = 1f;
    public bool stopInFog = false;

    private float timePassed = 0f;
    private bool overlappingPlayer = false;
    private Collider2D otherObject;
    private Rigidbody2D rb;
    private bool sleeping = false;
    private bool chasing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (overlappingPlayer)
        {
            Vector3 playerPos = otherObject.transform.position;
            Vector3 enemyPos = gameObject.transform.position;

            float distance = Vector3.Distance(playerPos, enemyPos);
     
            timePassed += Time.deltaTime;
            
            if (timePassed > delay)
            {
                chasing = true;
            }   
            
        } else
        {
            timePassed = 0f;
            chasing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (stopInFog && otherObject.gameObject.tag == "Fog")
        {
            overlappingPlayer = false;
            this.otherObject = null;
            sleeping = true;
        }


        if (!sleeping && otherObject.gameObject.tag == "Player")
        {
            overlappingPlayer = true;
            this.otherObject = otherObject;
        }
    }


    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            overlappingPlayer = false;
            this.otherObject = null;
        }

        if (otherObject.gameObject.tag == "Fog")
        {
            sleeping = false;
        }

    }

    private void FixedUpdate()
    {
        if (chasing)
        {
            Vector3 playerPos = otherObject.transform.position;
            Vector3 enemyPos = gameObject.transform.position;

            float step = moveSpeed * Time.deltaTime; // calculate distance to move
            Vector3 targetPos = Vector3.MoveTowards(enemyPos, playerPos, step);

            float distance = Vector3.Distance(playerPos, enemyPos);

            if (distance >= closestDistance)
                rb.MovePosition(targetPos);
        
        }
    }
}
