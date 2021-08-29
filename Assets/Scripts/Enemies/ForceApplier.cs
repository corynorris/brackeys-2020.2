using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    public enum ForceType
    {
        Linear,
        Exponential,
        InverseExponential
    }

    // Start is called before the first frame update
    public float innerRadius = 1f;
    public float outerRadius = 5f;
    public Detector detector;

    public float pullForce = 0.3f;
    public ForceType forceType;

    private float maxDistance = 0;

    private void Start()
    {
        if (!detector) Debug.Log(this.gameObject.name + " needs a detector!");

        maxDistance = detector.GetComponent<CircleCollider2D>().radius;
        detector.OnDetectedTagStay += Detector_OnDetectedTagStay;

    }

    private void OnDestroy()
    {
        detector.OnDetectedTagStay -= Detector_OnDetectedTagStay;
    }

    private void Detector_OnDetectedTagStay(object sender, Detector.DetectionInfoEventArgs e)
    {
        PlayerMovement playerMovement = e.detected.gameObject.GetComponent<PlayerMovement>();
        Vector3 playerPos = playerMovement.transform.position;
        Vector3 enemyPos = gameObject.transform.position;

        float distance = Mathf.Abs(Vector3.Distance(playerPos, enemyPos));

        if (distance < outerRadius && distance > innerRadius)
        {
            Vector3 targetDir = (enemyPos - playerPos).normalized;
            float distSqrt = Mathf.Pow((distance / maxDistance), 2);

            switch (forceType)
            {

                case ForceType.Exponential:
                    targetDir = targetDir * (1 - (distance / maxDistance));
                    break;

                case ForceType.InverseExponential:
                    targetDir = targetDir * (distance / maxDistance);
                    break;

            }

            playerMovement.ModifyForce(targetDir * (pullForce / 100));
        }

        
    }
}
