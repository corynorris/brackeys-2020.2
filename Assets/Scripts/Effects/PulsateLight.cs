using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PulsateLight : MonoBehaviour
{
    private Light2D myLight;
    public float variance= 0.2f;
    public float pulseSpeed = 1f; //here, a value of 0.5f would take 2 seconds and a value of 2f would take half a second
    private float targetIntensity = 1f;
    private float currentIntensity;
    private float maxIntensity;
    private float minIntensity;

    void Start()
    {
        myLight = GetComponent<Light2D>();
        minIntensity = myLight.intensity - variance;
        maxIntensity = myLight.intensity + variance;
        myLight.intensity = Random.Range(minIntensity, maxIntensity);
    }
    void Update()
    {
        currentIntensity = Mathf.MoveTowards(myLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
        if (currentIntensity >= maxIntensity)
        {
            currentIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            currentIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }
        myLight.intensity = currentIntensity;
    }
}
