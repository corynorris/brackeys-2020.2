using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Detector : MonoBehaviour
{
    public string detectTag = "Player";
    public bool stopDetectingInFog = false;

    private bool isInFog = false;
    private bool isOverlappingTag = false;

    public event EventHandler<DetectionInfoEventArgs> OnDetectedTagStop;
    public event EventHandler<DetectionInfoEventArgs> OnDetectedTagStart;
    public class DetectionInfoEventArgs : EventArgs
    {
        public Collider2D detected;
    }

    private void StartDetecting(Collider2D otherObject)
    {
        DetectionInfoEventArgs detectionInfoEventArgs = new DetectionInfoEventArgs { detected = otherObject };
        OnDetectedTagStart.Invoke(this, detectionInfoEventArgs);
    }

    private void StopDetecting()
    {
        DetectionInfoEventArgs detectionInfoEventArgs = new DetectionInfoEventArgs { detected = null };
        OnDetectedTagStop.Invoke(this, detectionInfoEventArgs);
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        string tag = otherObject.gameObject.tag;

        if (tag != detectTag && tag != "Fog") return;

        if (tag == detectTag)
        {
            isOverlappingTag = true;
        }

        if (tag == "Fog")
        {
            isInFog = true;
        }

        if (isInFog && stopDetectingInFog)
        {
            StopDetecting();
        }
        else if (isOverlappingTag)
        {
            StartDetecting(otherObject);

        }
    }


    private void OnTriggerExit2D(Collider2D otherObject)
    {
        string tag = otherObject.gameObject.tag;

        if (tag != detectTag && tag != "Fog") return;

        if (otherObject.gameObject.tag == "Fog")
        {
            isInFog = false;
        }


        if (otherObject.gameObject.tag == detectTag)
        {
            StopDetecting();
        }
  

    }


}
