using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DisableFarAway : MonoBehaviour
{

    public float maxDistance = 30f;

    private float nextActionTime = 0.0f;
    public float period = 0.5f;
    // Update is called once per frame
    private Light2D l;
    private Collider2D c;

    private void Start()
    {
        l = GetComponent<Light2D>();
        c = GetComponent<Collider2D>();

    }

    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            CheckIfEnableNeeded();
        }
    }
    
    private void SetLightAndColliderActive(bool isActive)
    {
        if (l)
        {
            l.enabled = isActive;
        }

        if (c)
        {
            c.enabled = isActive;
        }
    }

    private void SetChildrenActive(bool isActive)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var child = this.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(isActive);
        }
    }

    private void CheckIfEnableNeeded()
    {
        float distanceToPlayer = Vector3.Distance(Player.Instance.transform.position, transform.position);

        if (distanceToPlayer > maxDistance)
        {
            SetChildrenActive(false);
            SetLightAndColliderActive(false);
        } 
        else
        {
            SetChildrenActive(true);
            SetLightAndColliderActive(true);
        }

        
    }
}
