using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMProSortingOrder : MonoBehaviour
{

    [SerializeField] private bool runOnce;
    [SerializeField] private float positionOffsetY;

    private TextMeshPro textRenderer;

    private void Awake()
    {
        textRenderer = GetComponent<TextMeshPro>();
    }

    // lower y should be higher
    private void LateUpdate()
    {
        float precisionMultiplier = 5f;
        textRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * precisionMultiplier);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}