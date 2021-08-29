using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    
    public float moveSpeed = 1f;



    private Rigidbody2D rb;
    private bool wandering = true;
    private Detector detector;
    private Vector3 nextPos;
    private float curwalkTime = 0f;
    private float maxWalkTime = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        FollowPlayer followPlayer = GetComponent<FollowPlayer>();
        if (followPlayer != null)
        {
            Debug.Log("Will stop wandering when player detected because this csript has follow player as well.");
            detector = followPlayer.detector;
            detector.OnDetectedTagStart += Detector_OnDetectedTagStart;
            detector.OnDetectedTagStop += Detector_OnDetectedTagStop;
        }
    }

    private void OnDestroy()
    {
        if (detector)
        {
            detector.OnDetectedTagStart -= Detector_OnDetectedTagStart;
            detector.OnDetectedTagStop -= Detector_OnDetectedTagStop;
        }
    }


    private void Detector_OnDetectedTagStart(object sender, Detector.DetectionInfoEventArgs e)
    {
        wandering = false;
    }

    private void Detector_OnDetectedTagStop(object sender, Detector.DetectionInfoEventArgs e)
    {
        wandering = true;
    }

    private void Update()
    {
        curwalkTime += Time.deltaTime;
        if (curwalkTime >= maxWalkTime)
        {
            curwalkTime -= maxWalkTime;
            nextPos = this.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
        }
    }

    private void FixedUpdate()
    {
        if (!wandering) return;
        float step = moveSpeed * Time.deltaTime; // calculate distance to move
        Vector3 targetPos = Vector3.MoveTowards(transform.position, nextPos, step);

        rb.MovePosition(targetPos);
    }

}
