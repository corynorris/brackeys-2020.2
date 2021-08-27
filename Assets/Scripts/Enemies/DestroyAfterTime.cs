using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float effectDistance = 0f;
    public float destroyAfter = 1f;
    public bool destroyOnWallCollision = true;

    private bool overlappingPlayer = false;
    private Collider2D otherObject;


    private void Start()
    {
        

    }

    private void Update()
    {
        if (overlappingPlayer)
        {
            Vector3 playerPos = otherObject.transform.position;
            Vector3 enemyPos = gameObject.transform.position;

            float distance = Vector3.Distance(playerPos, enemyPos);

            if (distance <= effectDistance)
            {
                Destroy(gameObject, destroyAfter);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D otherObject)
    {

        if (otherObject.gameObject.tag == "Player")
        {
            overlappingPlayer = true;
            this.otherObject = otherObject;
            
        }
         
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (overlappingPlayer && destroyOnWallCollision && collision.gameObject.tag != "Player")
        //{
        //    Vector3 playerPos = collision.gameObject.transform.position;
        //    Vector3 enemyPos = gameObject.transform.position;

        //    float distance = Vector3.Distance(playerPos, enemyPos);

        //    Debug.Log("another collision, distance: " + distance);
        //    if (distance < 2f)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }


    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            overlappingPlayer = false;
            this.otherObject = null;
        }

    }

}
