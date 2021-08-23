using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    public static Player Instance { get; private set; }
    private CircleCollider2D circleCollider;

    private Vector3 forward = Vector3.down;
    [SerializeField]
    private GameObject gameObject;

    private Animator body;
    private Animator head;
    private Animator weapon;

    private Volume volume;

    private Volume v;
    private Vignette vg;

    private void Awake()
    {
        Instance = this;
        inventory = new Inventory();
        circleCollider = GetComponent<CircleCollider2D>();

        // Get all animators
        body = transform.Find("body").GetComponent<Animator>();
        head = transform.Find("head").GetComponent<Animator>();
        weapon = transform.Find("weapon").GetComponent<Animator>();
        volume = FindObjectOfType<Volume>();
    }

    private void Start()
    {
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);


        v = gameObject.GetComponent<Volume>();
        v.profile.TryGet(out vg);

    }
    float lerpTime = 1f;
    float currentLerpTime;
    float targetIntensity = 0.2f;
    float startIntensity = 0.2f;

    private void Update()
    {
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float perc = currentLerpTime / lerpTime;
        vg.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, perc);
    }

    public Vector3 GetCenter()
    {
        return circleCollider.bounds.center;
    }


    public bool CanMove()
    {
        return body.GetCurrentAnimatorStateInfo(0).IsName("Movement") || body.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

    public void SetSpeed(float speed)
    {
        body.SetFloat("Speed", speed);
    }

    public void SetForwardDirection(Vector3 forward)
    {
        this.forward = forward;

        SetAllAnimatorFloats("Horizontal", forward.x);
        SetAllAnimatorFloats("Vertical", forward.y);

    }

    public Vector3 GetForwardDirection()
    {
        return forward;
    }

    public void SetAllAnimatorFloats(string name, float value)
    {
        body.SetFloat(name, value);
        head.SetFloat(name, value);
        weapon.SetFloat(name, value);
    }


    public void TriggerAllAnimators(string name)
    {
        weapon.SetTrigger(name);
        body.SetTrigger(name);
        head.SetTrigger(name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item") {
            ItemWorld itemWorld = collision.gameObject.GetComponent<ItemWorld>();
            if (itemWorld)
            {
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }
        }
    }

    public void Blind()
    {
        startIntensity = vg.intensity.value;
        targetIntensity = 1f;
        currentLerpTime = 0f;
    }

    public void UnBlind()
    {
        startIntensity = vg.intensity.value;
        targetIntensity = 0.2f;
        currentLerpTime = 0f;

    }

}
