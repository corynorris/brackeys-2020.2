using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField]
    LevelController levelControler;

    private Health health;

    [Header("Game Over")]
    [SerializeField] private UI_GameOver gameOver;

    [Header("Inventory")]
    [SerializeField] private UI_Inventory uiInventory;

    private Inventory inventory;

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
    private float currentLerpTime = 0;
    private float targetIntensity = 0.2f;
    private float perc = 0;
    private float startIntensity = 0.2f;


    [Header("Some weird menu stuff")]
    public bool menuOpen;
    public bool menuAvaliable;
    private Vector3 oldPos;

    private ShipStationController menu;
    private CircleCollider2D circleCollider;
    private Vector3 forward = Vector3.down;
    private Animator body;
    private Animator head;
    private Animator weapon;


    [SerializeField]
    private AudioClip pickupItem;

    [SerializeField]
    private AudioClip closeDoor;


    [SerializeField]
    private AudioClip outsideAmbience;


    [SerializeField]
    private AudioClip insideAmbience;

    public bool isInside;

    private void Awake()
    {
        isInside = false;
        Instance = this;
        inventory = new Inventory(UseItem);
        circleCollider = GetComponent<CircleCollider2D>();
        
        // Get all animators
        body = transform.Find("body").GetComponent<Animator>();
        head = transform.Find("head").GetComponent<Animator>();
        weapon = transform.Find("weapon").GetComponent<Animator>();


        // Add some items
        inventory.AddItem(new Item { amount = 1, itemType = Item.ItemType.Light });
        inventory.AddItem(new Item { amount = 1, itemType = Item.ItemType.Oxygen });
        inventory.AddItem(new Item { amount = 1, itemType = Item.ItemType.Oxygen });
        inventory.AddItem(new Item { amount = 1, itemType = Item.ItemType.HoverBoard });
        health = GetComponent<Health>();
    }

    private void Start()
    {
        uiInventory.SetInventory(inventory);                    
        uiInventory.SetPlayer(this);

        volume = FindObjectOfType<Volume>();
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

      
        if (vg != null) { 
            vg.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, perc);
        }

        if (menuAvaliable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenMenu();

            }
        }
        else if (menuOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseMenu();
            }
        }




    }

    public void UseItem(Item item)
    {
        LayerMask mask = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));

        switch (item.itemType)
        {
            case Item.ItemType.HoverBoard:
            case Item.ItemType.Light:
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                ItemWorld.DropItemInDirection(GetCenter(), duplicateItem, forward, mask);
                return;
            case Item.ItemType.Oxygen:
                LevelController.GetInstance().ResetOxygen();
                return;
            default:                 
                Debug.LogWarning("Add logic to use item in Player `UseItem` function for item: " + item.itemType); 
                return;
        }
    }
    public bool IsAlive()
    {
        return health.IsAlive();
    }

    private void Health_OnDied(object sender, Health.DamageInfoEventArgs e)
    {
        Die();
    }

    public void Die()
    {
        body.SetTrigger("Die");
        gameOver.Show();
    }

    private void Health_OnTookDamage(object sender, Health.DamageInfoEventArgs e)
    {
        body.SetTrigger("Damaged");
        LevelController.GetInstance().RemoveOxygen((int)e.damage);
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
                if (inventory.AddItem(itemWorld.GetItem())) { 
                    itemWorld.DestroySelf();
                    Utils.spawnAudio(gameObject, pickupItem, 0.35f);
                }
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
            UI_Notification.Instance.Notify("Press 'e' to use", 3f);
            Debug.Log("MENU AVLIABLE");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Station")
        {
            if (menuOpen)
            {
                CloseMenu();
            }                       
            menuAvaliable = false;
            this.menu.highlightStation(false);
            this.menu = null;
            UI_Notification.Instance.Clear();
        }
    }

    public void OpenMenu()
    {
        Debug.Log("OPENING MENU");
        if (this.menu.isExit)
        {
            ExitShip();
        } else if(this.menu.isEntrance)
        {
            EnterShip();
        }
        else
        {
            this.menu.turnOnMenu(this);
            menuOpen = true;
            menuAvaliable = false;
        }
    }

    public void CloseMenu()
    {
        if (! this.menu.isExit && !this.menu.isEntrance)
        {
            menuOpen = false;
            this.menu.turnOffMenu();
            menuAvaliable = true;
        }        
    }

    public void Blind()
    {
        lerpTime = lerpInTime * perc;
        currentLerpTime = 0;
        startIntensity = vg.intensity.value;
        targetIntensity = maxIntensity / levelControler.GetSuiteGogglesEffect();
    }

    public void UnBlind()
    {
        lerpTime = lerpOutTime * perc;
        currentLerpTime = 0;
        startIntensity = vg.intensity.value;
        targetIntensity = minIntensity;
    }

    public void EnterShip()
    {
        levelControler.PauseEnergyConsumption();
        levelControler.ResetOxygen();        
        levelControler.PauseOxygenConsumption();
        levelControler.ProcessNutrients();
        levelControler.PauseFoodConsumption();
        levelControler.GetShip().PauseDeacay();
        
        levelControler.DepositScrap();
        oldPos = Instance.transform.position;
        //SceneManager.LoadScene("Spaceship", LoadSceneMode.Additive);
        Instance.transform.position = new Vector3(-57, -15, 0);
        Utils.spawnAudio(gameObject, closeDoor, 0.45f);
        MusicManager.getInstance().updateMusic(insideAmbience);
        isInside = true;
    }

    public void ExitShip()
    {
        Instance.transform.position = oldPos;
        //SceneManager.UnloadSceneAsync("Spaceship");
        levelControler.ResumeEnergyConsumption();
        levelControler.RefillFood();
        levelControler.ResumeFoodConsumption();        
        levelControler.ResumeOxygenConsumption();
        levelControler.GetShip().ResumeDeacay();
        FindObjectOfType<SpawnManager>().RestartSpawners();
        Utils.spawnAudio(gameObject, closeDoor, 0.4f);
        MusicManager.getInstance().updateMusic(outsideAmbience);
        isInside = false;
        //put back outside ship
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

}
