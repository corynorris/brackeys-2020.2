using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagePlayer))]

public class Explode : MonoBehaviour
{
    public string triggerName = "explode";
    private Animator animator;

    private void Start()
    {
        DamagePlayer damagePlayer = GetComponent<DamagePlayer>();
        damagePlayer.OnDamagedPlayer += DamagePlayer_OnDamagedPlayer;
        animator = GetComponent<Animator>();
    }

    private void DamagePlayer_OnDamagedPlayer(object sender, System.EventArgs e)
    {
        Boom();
    }

    private void Boom()
    {
        animator.SetTrigger("explode");
        Destroy(gameObject, 1f);
    }
}
