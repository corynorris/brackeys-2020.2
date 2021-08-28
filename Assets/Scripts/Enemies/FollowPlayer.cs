using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class FollowPlayer : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float closestDistance = 1f;
    public Detector detector;

    private bool chasing = false;
  
    private Rigidbody2D rb;
    private Collider2D otherObject;

    private void Start()
    {
        if (!detector) Debug.Log(this.gameObject.name + " needs a detector!");

        rb = GetComponent<Rigidbody2D>();
        detector.OnDetectedTagStart += Detector_OnDetectedTagStart;
        detector.OnDetectedTagStop += Detector_OnDetectedTagStop;
    }
    private void OnDestroy()
    {
        detector.OnDetectedTagStart -= Detector_OnDetectedTagStart;
        detector.OnDetectedTagStop -= Detector_OnDetectedTagStop;
    }


    private void Detector_OnDetectedTagStart(object sender, Detector.DetectionInfoEventArgs e)
    {
        Debug.Log("Started chasing");
        otherObject = e.detected;
        chasing = true;
    }

    private void Detector_OnDetectedTagStop(object sender, Detector.DetectionInfoEventArgs e)
    {
        Debug.Log("Stopped chasing");
        otherObject = null;
        chasing = false;
    }
  

    private void FixedUpdate()
    {
        if (chasing && otherObject != null)
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
