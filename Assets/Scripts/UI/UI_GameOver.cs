using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    // Start is called before the first frame update

    public Color targetColor = new Color(1, 1, 1,1);
    
    private Transform panel;
    private GameObject gameOverText;

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeTo(1.0f, 1.0f));
    }


    private void Start()
    {
        panel = transform.Find("panel");
        gameOverText = panel.Find("gameOverText").gameObject;

        panel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        gameOverText.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = panel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(targetColor.r, targetColor.g, targetColor.b, Mathf.Lerp(alpha, aValue, t));
            panel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        gameOverText.SetActive(true);

    }
}
