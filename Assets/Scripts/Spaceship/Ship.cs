using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{    
    [SerializeField] float decayRate = 1.0f;
    [SerializeField] float decayAmount = 1.0f;
    [SerializeField] float hullHealth = 1.0f;
    [SerializeField] float maxHullHealth = 100.0f;
    [SerializeField] float repairAmount = 10.0f;
    [SerializeField] float reactorHealth = 0.0f;
    [SerializeField] float reactorHealthMax = 100.0f;
    [SerializeField] float wingHealth = 0.0f;
    [SerializeField] float wingHealthMax = 100.0f;
    [SerializeField] float cockpitHealth = 0.0f;
    [SerializeField] float cockpitHealthMax = 100.0f;
    [SerializeField] float thrustersHealth = 0.0f;
    [SerializeField] float thrustersHealthMax = 100.0f;

    [SerializeField] float hullRepairCost = 1.0f;
    [SerializeField] float reactorRepairCost = 30.0f;
    [SerializeField] float wingRepairCost = 30.0f;
    [SerializeField] float cockpitRepairCost = 30.0f;
    [SerializeField] float thrustersRepairCost = 30.0f;
    float timeTracker = 0f;
    private bool decaying = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResumeDeacay();
    }

    public bool WingStatus()
    {
        return wingHealth == wingHealthMax;
    }

    public bool CockpitStatus()
    {
        return cockpitHealth == cockpitHealthMax;
    }

    public bool ReactorStatus()
    {
        return reactorHealth == reactorHealthMax;
    }

    public bool ThrustersStatus()
    {
        return thrustersHealth == thrustersHealthMax;
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
            if(hullHealth <= 0)
            {
                FindObjectOfType<Player>().Die();
            }
            /*foreach (ShipPart part in parts)
            {
                if(part.HasDecay() && part.GetHealth() > 0)
                    part.Decay();
            } */
        }
        
    }

    public void RestoreHull()
    {
        hullHealth = hullHealth + repairAmount ;
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

    internal void RestoreReactorHealthMax()
    {
        reactorHealth = reactorHealthMax;
    }

    internal void RestoreCockpitHealthMax()
    {
        cockpitHealth = cockpitHealthMax;
    }

    internal void RestoreWingsHealthMax()
    {
        wingHealth = wingHealthMax;
    }

    internal void RestoreThrustersHealthMax()
    {
        thrustersHealth = thrustersHealthMax;
    }
}
