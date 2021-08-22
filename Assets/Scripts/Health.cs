using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler OnDied;

    public float maxHealth;
    private float curHealth;

    private void Awake()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        maxHealth -= damage;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        
        if (curHealth == 0)
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }
}
