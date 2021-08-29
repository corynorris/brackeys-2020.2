using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamagePlayer : MonoBehaviour
{

    public float damage = 5f;
    public float effectDistance = 1f;
    public float frequency = 1f;


    private float timePassed = 0f;
    private bool overlappingPlayer = false;
    private Collider2D otherObject;

    public event EventHandler OnDamagedPlayer;
    public Detector detector;

    private void Start()
    {
        if (!detector) Debug.Log(this.gameObject.name + " needs a detector!");
        
        detector.OnDetectedTagStart += Detector_OnDetectedTagStart;
        detector.OnDetectedTagStop += Detector_OnDetectedTagStop;
        
        timePassed = frequency;
    }


    private void OnDestroy()
    {
        detector.OnDetectedTagStart -= Detector_OnDetectedTagStart;
        detector.OnDetectedTagStop -= Detector_OnDetectedTagStop;
    }


    private void Detector_OnDetectedTagStart(object sender, Detector.DetectionInfoEventArgs e)
    {
        otherObject = e.detected;
        overlappingPlayer = true;
    }

    private void Detector_OnDetectedTagStop(object sender, Detector.DetectionInfoEventArgs e)
    {
        otherObject = null;
        overlappingPlayer = false;
    }


    private void Update()
    {
        if (overlappingPlayer)
        {
            Vector3 playerPos = otherObject.transform.position;
            Vector3 enemyPos = gameObject.transform.position;

            float distance = Mathf.Abs(Vector3.Distance(playerPos, enemyPos));


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


}
