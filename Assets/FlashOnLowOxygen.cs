using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnLowOxygen : MonoBehaviour
{
    public int lowOxygenCount = 30;
    // Start is called before the first frame update
    public Color flashColor;

    private Color originalColor;
    private SpriteRenderer sprite;
    private bool invoked = false;

    private void Start()
    {
        sprite = transform.Find("body").GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    private void Update()
    {
        if (LevelController.GetInstance().GetPlayerOxygen() < lowOxygenCount && !invoked)
        {
            StartCoroutine(Flash(0.5f));
            invoked = true;
        }
        
        if (LevelController.GetInstance().GetPlayerOxygen() >= lowOxygenCount)
        {
            StopAllCoroutines();
            invoked = false;
        }    
    }

    IEnumerator Flash(float intervalTime)
    {

        while (true)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(intervalTime);
            sprite.color = originalColor;
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
