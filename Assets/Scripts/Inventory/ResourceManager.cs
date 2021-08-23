using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    private float shipHealth;
    [SerializeField]
    private ProgressBar shipHealthBar;
    private float oxygen;
    [SerializeField]
    private ProgressBar oxygenBar;
    private float energy;
    [SerializeField]
    private ProgressBar energyBar;
    private float scrap;
    [SerializeField]
    private ResourceCounter scrapCounter;
    private float food;
    [SerializeField]
    private ResourceCounter foodCounter;

    private static ResourceManager _instance;
    public static ResourceManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        updateShipHealth(1);
        updateOxygen(1);
        updateEnergy(1);
        updateScrap(50);
        updateFood(100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateShipHealth(float shipHealth)
    {
        this.shipHealth += shipHealth;
        this.shipHealthBar.setProgress(shipHealth);
    }

    public void updateOxygen(float oxygen)
    {
        this.oxygen += oxygen;
        this.oxygenBar.setProgress(oxygen);
    }

    public void updateEnergy(float energy)
    {
        this.energy += energy;
        this.energyBar.setProgress(energy);
    }

    public void updateScrap(float scrap)
    {
        this.scrap += scrap;
        this.scrapCounter.updateText(scrap);
    }

    public void updateFood(float food)
    {
        this.food += food;
        this.foodCounter.updateText(food);
    }

}
