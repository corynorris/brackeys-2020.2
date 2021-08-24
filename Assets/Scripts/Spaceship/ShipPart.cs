using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{

    [SerializeField] float health;
    [SerializeField] float healthDecay;    
    [SerializeField] float repairCost;
    [SerializeField] bool critical;
    [SerializeField] bool required;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Decay()
    {
        health = health - healthDecay;
    }

    public bool HasDecay()
    {
        return healthDecay > 0;
    }

    public bool IsCritical()
    {
        return critical;
    }

    public float GetHealth()
    {
        return health;
    }
}
