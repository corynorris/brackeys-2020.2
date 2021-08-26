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
    void Start()
    {
        health.OnDied += Health_OnDied;
    }

    private void Health_OnDied(object sender, Health.DamageInfoEventArgs e)
    {
        Destroy(this.gameObject);
    }
}
