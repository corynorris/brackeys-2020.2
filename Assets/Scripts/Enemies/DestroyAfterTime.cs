using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public Detector detector;
    public float destroyAfter = 1f;
    public bool destroyOnWallCollision = true;
    
    private void Start()
    {
        if (!detector) Debug.Log(this.gameObject.name + " needs a detector!");

        detector.OnDetectedTagStart += Detector_OnDetectedTagStart;
    }

    private void OnDestroy()
    {
        detector.OnDetectedTagStart -= Detector_OnDetectedTagStart;
    }

    private void Detector_OnDetectedTagStart(object sender, Detector.DetectionInfoEventArgs e)
    {
        Destroy(this.gameObject, destroyAfter);
    }
}
