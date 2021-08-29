using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Win : MonoBehaviour
{
    private Transform panel;
    private GameObject gameOverText;

    public Color targetColor = new Color(1, 1, 1, 1);

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeTo(1.0f, 1.0f));
    }

    private void Start()
    {
        panel = transform.Find("panel");
        gameOverText = panel.Find("youWinText").gameObject;

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

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
