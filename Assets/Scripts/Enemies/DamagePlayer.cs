using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(CircleCollider2D))]
public class DamagePlayer : MonoBehaviour
{

    public float damage = 5f;
    public float effectDistance = 1f;
    public float frequency = 1f;


    private float timePassed = 0f;
    private bool overlappingPlayer = false;
    private Collider2D otherObject;

    public event EventHandler OnDamagedPlayer;

    private void Update()
    {
        if (overlappingPlayer)
        {
            Vector3 playerPos = otherObject.transform.position;
            Vector3 enemyPos = gameObject.transform.position;

            float distance = Vector3.Distance(playerPos, enemyPos);


            if (distance <= effectDistance)
            {
                timePassed += Time.deltaTime;
                if (timePassed > frequency)
                {
                    timePassed -= frequency;
                    Health health = otherObject.GetComponent<Health>();
                    health.TakeDamage(damage);
                    OnDamagedPlayer?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {

        overlappingPlayer =  otherObject.gameObject.tag == "Player" ? true : false;
        if (overlappingPlayer)
        {
            this.otherObject = otherObject;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        overlappingPlayer = false;
        this.otherObject = null;
    }

  
}
