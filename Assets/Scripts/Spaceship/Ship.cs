using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{    
    [SerializeField] float decayRate = 1.0f;
    [SerializeField] float decayAmount = 1.0f;
    [SerializeField] float hullHealth = 1.0f;
    [SerializeField] float maxHullHealth = 100.0f;

    [SerializeField] float reactorHealth = 0.0f;
    [SerializeField] float wingHealth = 0.0f;
    [SerializeField] float cockpitHealth = 0.0f;
    [SerializeField] float thrustersHealth = 0.0f;

    [SerializeField] float hullRepairCost = 1.0f;
    [SerializeField] float reactorRepairCost = 30.0f;
    [SerializeField] float wingRepairCost = 30.0f;
    [SerializeField] float cockpitRepairCost = 30.0f;
    [SerializeField] float thrustersRepairCost = 30.0f;
    float timeTracker = 0f;
    private bool decaying = false;
    


    // Start is called before the first frame update
    void Start()
    {
        ResumeDeacay();
    }

    public float GetHullRepairCost()
    {
        return hullRepairCost;
    }
    public float GetReactorRepairCost()
    {
        return reactorRepairCost;
    }

    public float GetWingRepairCost()
    {
        return wingRepairCost;
    }

    public float GetCockpitRepairCost()
    {
        return cockpitRepairCost;
    }

    public float GetThrustersRepairCost()
    {
        return thrustersRepairCost;
    }

    // Update is called once per frame
    void Update()
    {        
        timeTracker += Time.deltaTime;

        if (timeTracker >= decayRate && decaying)
        {
            timeTracker = timeTracker % decayRate;
            hullHealth = hullHealth - decayAmount;
            /*foreach (ShipPart part in parts)
            {
                if(part.HasDecay() && part.GetHealth() > 0)
                    part.Decay();
            } */
        }
        
    }

    public void RestoreHull()
    {
        hullHealth = maxHullHealth;
    }

    public void PauseDeacay()
    {
        decaying = false;
    }

    public void ResumeDeacay()
    {
        timeTracker = 0f;
        decaying = true;
    }

    public float GetHealth()
    {
        return hullHealth;
    }
}
