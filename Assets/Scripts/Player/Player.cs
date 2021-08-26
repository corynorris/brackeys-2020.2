using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{

    [Header("Game Over")]
    [SerializeField] private UI_GameOver gameOver;

    [Header("Inventory")]
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    public static Player Instance { get; private set; }
    private CircleCollider2D circleCollider;

    private Vector3 forward = Vector3.down;

    private Animator body;
    private Animator head;
    private Animator weapon;

    private Health health;

    [Header("Blindness")]
    [SerializeField] private GameObject Volume;
    [SerializeField] private float lerpInTime = 1f;
    [SerializeField] private float lerpOutTime = 2f;
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 1f;

    private Volume volume;
    private Volume v;
    private Vignette vg;
    private float lerpTime = 1;
    private float currentLerpTime;
    private float targetIntensity = 0.2f;
    private float perc = 0;
    private float startIntensity;

    private void Awake()
    {
        Instance = this;
        inventory = new Inventory(UseItem);
        circleCollider = GetComponent<CircleCollider2D>();

        // Get all animators
        body = transform.Find("body").GetComponent<Animator>();
        head = transform.Find("head").GetComponent<Animator>();
        weapon = transform.Find("weapon").GetComponent<Animator>();
        volume = FindObjectOfType<Volume>();


        // Add some items
        inventory.AddItem(new Item { amount = 1, itemType = Item.ItemType.Light });
        inventory.AddItem(new Item { amount = 1, itemType = Item.ItemType.Oxygen });
        inventory.AddItem(new Item { amount = 1, itemType = Item.ItemType.Oxygen });
        health = GetComponent<Health>();


    }

    private void Start()
    {
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);

        v = Volume.GetComponent<Volume>();
        v.profile.TryGet(out vg);

        health.OnTookDamage += Health_OnTookDamage;
        health.OnDied += Health_OnDied;

    }



    private void Update()
    {
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        perc = 0f;
        if (lerpTime > 0)
        {
            perc = currentLerpTime / lerpTime;
        }

        vg.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, perc);

    }

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Light:
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                ItemWorld.DropItemInDirection(GetCenter(), duplicateItem, forward);
                return;
            case Item.ItemType.Oxygen:

                health.TakeDamage(10);
                return;
            default:                 
                Debug.LogWarning("Add logic to use item in Player `UseItem` function for item: " + item.itemType); 
                return;
        }
    }

    private void Health_OnDied(object sender, Health.DamageInfoEventArgs e)
    {
        body.SetTrigger("Die");
        gameOver.Show();
    }

    private void Health_OnTookDamage(object sender, Health.DamageInfoEventArgs e)
    {
        body.SetTrigger("Damaged");
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
        lerpTime = lerpInTime * perc;
        currentLerpTime = 0;
        startIntensity = vg.intensity.value;
        targetIntensity = maxIntensity;
    }

    public void UnBlind()
    {
        lerpTime = lerpOutTime * perc;
        currentLerpTime = 0;
        startIntensity = vg.intensity.value;
        targetIntensity = minIntensity;
    }

}
