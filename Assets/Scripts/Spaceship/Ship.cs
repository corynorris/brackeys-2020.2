using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] ShipPart[] parts;
    [SerializeField] float decayRate = 1.0f;

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

    // Update is called once per frame
    void Update()
    {        
        timeTracker += Time.deltaTime;

        if (timeTracker >= decayRate && decaying)
        {
            timeTracker = timeTracker % decayRate;
            foreach (ShipPart part in parts)
            {
                if(part.HasDecay() && part.GetHealth() > 0)
                    part.Decay();
            }            
        }
        
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
        float health = 100;

        foreach (ShipPart part in parts)
        {
            if (part.IsCritical())
                health = Mathf.Min(health, part.GetHealth());
        }

        return health;
    }
}
