using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagePlayer))]

public class FlashOnDamageDealt : MonoBehaviour
{
    public Color flashColor;

    private Color originalColor;
    private SpriteRenderer sprite;
    private void Start()
    {
        DamagePlayer damagePlayer = GetComponent<DamagePlayer>();
        damagePlayer.OnDamagedPlayer += DamagePlayer_OnDamagedPlayer;
        sprite = transform.Find("sprite").GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    private void DamagePlayer_OnDamagedPlayer(object sender, System.EventArgs e)
    
        {
            StartCoroutine(Flash(0.1f));
        }

    IEnumerator Flash(float intervalTime)
    {

        for (int n = 0; n < 2; n++)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(intervalTime);
            sprite.color = originalColor;
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
