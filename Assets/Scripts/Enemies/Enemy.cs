using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        
        health = GetComponent<Health>();
    }
    private void Start()
    {
        health.OnTookDamage += Health_OnTookDamage;
        health.OnDied += Health_OnDied;
    }

    private void OnDestroy()
    {
        health.OnTookDamage -= Health_OnTookDamage;
        health.OnDied -= Health_OnDied;
    }
    private void Health_OnTookDamage(object sender, Health.DamageInfoEventArgs e)
    {
        Debug.Log("took damage: " + e.damage);
    }

    private void Health_OnDied(object sender, Health.DamageInfoEventArgs e)
    {
        Destroy(this.gameObject);
    }
}
