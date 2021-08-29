using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Notification : MonoBehaviour
{
    public static UI_Notification Instance { get; private set; }
    TextMeshProUGUI notificationText;


    private bool messageShowing = false;
    private float elapsed = 0;
    private float duration = 0f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        notificationText = transform.Find("notificationText").GetComponent<TextMeshProUGUI>();
    }

    public void Notify(string message, float duration = 10f)
    {
        notificationText.text = message;
        this.duration = duration;
        this.elapsed = 0;
        this.messageShowing = true;
    }
    public void Clear()
    {
        notificationText.text = "";
    }

    void Update()
    {
        if (messageShowing)
        {
            elapsed += Time.deltaTime;
            if (elapsed > duration)
            {
                notificationText.text = "";
                messageShowing = false;
            }
        }
    }
}
