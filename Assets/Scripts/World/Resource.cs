using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public Item item;
        private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDied += Health_OnDied;
    }

    private void Health_OnDied(object sender, Health.DamageInfoEventArgs eventArgs)
    {
        ItemWorld.DropItemInDirection(transform.position, item, eventArgs.direction);
        Destroy(this.gameObject);
    }
}
