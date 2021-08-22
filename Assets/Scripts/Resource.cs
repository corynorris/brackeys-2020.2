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

    private void Health_OnDied(object sender, System.EventArgs e)
    {
        ItemWorld.DropItem(transform.position, item);
        Destroy(this.gameObject);
    }
}
