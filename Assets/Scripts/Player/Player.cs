using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{


    [Header("Inventory")]
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    public static Player Instance { get; private set; }
    private CircleCollider2D circleCollider;

    private Vector3 forward = Vector3.down;

    private Animator body;
    private Animator head;
    private Animator weapon;



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
    public bool menuOpen;
    public bool menuAvaliable;

    private ShipStationController menu;

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

        v = Volume.GetComponent<Volume>();
        v.profile.TryGet(out vg);

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

    public Vector3 GetCenter()
    {
        return circleCollider.bounds.center;
    }


    public bool CanMove()
    {
        return body.GetCurrentAnimatorStateInfo(0).IsName("Movement") || body.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !menuOpen;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Station")
        {
            this.menu = collision.gameObject.GetComponentInParent<ShipStationController>();
            menuAvaliable = true;
            this.menu.highlightStation(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Station")
        {
            closeMenu();
            this.menu = null;
            menuAvaliable = false;
            this.menu.highlightStation(false);
        }
    }

    public void openMenu()
    {
        if (this.menu.isExit)
        {
            exitShip();
        } else if(this.menu.isEntrance)
        {
            enterShip();
        }
        else
        {
            this.menu.turnOnMenu();
            menuOpen = true;
            menuAvaliable = false;
        }
    }

    public void closeMenu()
    {
        menuOpen = false;
        this.menu.turnOffMenu();
        menuAvaliable = true;
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

    public void enterShip()
    {
        SceneManager.LoadScene("Spaceship", LoadSceneMode.Additive);
        Instance.transform.position = new Vector3(0, 0, 0);
    }

    public void exitShip()
    {
        SceneManager.UnloadSceneAsync("Spaceship");
        //put back outside ship
    }

}
