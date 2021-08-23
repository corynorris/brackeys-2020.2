using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler<DamageInfoEventArgs> OnDied;
    public event EventHandler<DamageInfoEventArgs> OnTookDamage;
    public class DamageInfoEventArgs : EventArgs
    {
        public float damage;
        public Vector3 direction;
    }


    public float maxHealth;
    private float curHealth;

    private void Awake()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        TakeDamage(damage, Vector3.zero);
    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        maxHealth -= damage;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);


        DamageInfoEventArgs damageInfoEventArgs = new DamageInfoEventArgs { damage = damage, direction = direction };
            
        if (curHealth == 0)
        {
            OnDied?.Invoke(this, damageInfoEventArgs);
        } else
        {
            OnTookDamage?.Invoke(this, damageInfoEventArgs);
        }
    }
}
