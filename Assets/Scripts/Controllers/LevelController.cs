using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    private static LevelController _instance = null;

    [SerializeField] Ship ship;

    private Player player;

    [SerializeField] float foodConsumptionRatio = 10;

    [SerializeField]  private float playerOxygen;
    [SerializeField]  private float maxPlayerOxygen;

    [SerializeField] private float playerEnergy;
    [SerializeField] private float maxPlayerEnergy;

    [SerializeField]  private float playerFood;
    [SerializeField]  private float maxPlayerFood;

    //add energy

    [SerializeField]  private float reserveFood;
    [SerializeField]  private float reserveScrap;

    bool consumeFood;
    [SerializeField]  float foodConsumptionRate;
    [SerializeField]  float foodConsumptionPeriod = 1f;

    bool consumeOxygen;
    [SerializeField] float oxygenConsumptionRate;
    [SerializeField]  float oxygenConsumptionPeriod = 1f;

    bool consumeEnergy;
    [SerializeField] float energyConsumptionRate;
    [SerializeField] float energyConsumptionPeriod = 1f;

    float foodConsumptionTimeTracker = 0f;
    float oxygenConsumptionTimeTracker = 0f;
    float energyConsumptionTimeTracker = 0f;

    private ResourceManager resourceManager;

    public int suitTankLvl { get; set; }
    public int suitGogglesLvl { get; set; }
    public int suitBootsLvl { get; set; }

    [SerializeField] float[] suitTankLvlMultiplier;
    [SerializeField] float[] suitBootsLvlMultiplier;
    [SerializeField] float[] suitGogglesLvlMultiplier;
    [SerializeField] int[] foodProcessingLvlMultiplier;

    [SerializeField] int[] suitTankCost;
    [SerializeField] int[] suitGogglesCost;
    [SerializeField] int[] suitBootsCost;

    [SerializeField] int[] foodProcessingCost;


    [SerializeField]
    private AudioClip refillOxygenClip;

    [SerializeField] float oxygenTankCost;
    [SerializeField] float batteryPackCost;
    public int foodProcessingLvl { get; set; }

    public static LevelController GetInstance() {
        return _instance;
    }

    public Ship GetShip()
    {
        return ship;
    }

    public float GetOxygenTankCost()
    {
        return oxygenTankCost;
    }

    public float GetBatteryPackCost()
    {
        return batteryPackCost;
    }

    public float[] GetSuiteTankLvlMultiplier()
    {
        return suitTankLvlMultiplier;
    }

    public int[] GetSuiteTankLvlCost()
    {
        return suitTankCost;
    }

    public float[] GetSuiteBootsLvlMultiplier()
    {
        return suitBootsLvlMultiplier;
    }

    public int[] GetSuiteBootsLvlCost()
    {
        return suitBootsCost;
    }

    public float[] GetSuiteGogglesLvlMultiplier()
    {
        return suitGogglesLvlMultiplier;
    }

    public int[] GetSuiteGogglesLvlCost()
    {
        return suitGogglesCost;
    }

    public int[] GetFoodProcessingLvlCost()
    {
        return foodProcessingCost;
    }

    public int[] GetFoodProcessingLvlMultiplier()
    {
        return foodProcessingLvlMultiplier;
    }

    public float GetBootsMultiplier()
    {
        return suitBootsLvlMultiplier[suitBootsLvl - 1];
    }


    public float NextSuiteTankLvlCost()
    {
        if (suitTankLvl < suitTankCost.Length)
            return suitTankCost[suitTankLvl];
        else
            return -1;
    }

    public float NextSuiteBootsLvlCost()
    {
        if (suitBootsLvl < suitBootsCost.Length)
            return suitBootsCost[suitBootsLvl];
        else
            return -1;
    }

    public float NextSuiteGogglesLvlCost()
    {
        if (suitGogglesLvl < suitGogglesCost.Length)
            return suitGogglesCost[suitGogglesLvl];
        else
            return -1;
    }

    public float GetSuiteGogglesEffect()
    {
        return suitGogglesLvlMultiplier[suitGogglesLvl - 1];
    }

    public float GetMaxOxygen()
    {
        return maxPlayerOxygen * suitTankLvlMultiplier[suitTankLvl - 1];
    }

    public void AddOxygen(int oxygen)
    {
        playerOxygen = Mathf.Min(playerOxygen + oxygen, GetMaxOxygen());

    }

    public void RemoveOxygen(int oxygen)
    {
        playerOxygen = Mathf.Max(playerOxygen - oxygen, 0);

    }


    public float NextFoodProcessingLvlCost()
    {
        if (foodProcessingLvl < foodProcessingCost.Length)
            return foodProcessingCost[foodProcessingLvl];
        else
            return -1;
    }

    private void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = ResourceManager.GetInstance();
        foodProcessingLvl = 1;
        suitGogglesLvl = 1;
        suitBootsLvl = 1;
        suitTankLvl = 1;
        ResumeFoodConsumption();
        ResumeOxygenConsumption();
        ResumeEnergyConsumption();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(consumeFood)
            ConsumeFood();
        if(consumeOxygen)
            ConsumeOxygen();
        if (consumeEnergy)
            ConsumeEnergy();

        UpdateResourceManager();
    }

    private void UpdateResourceManager()
    {
        //resourceManager.updateFood(playerFood / maxPlayerFood);
        resourceManager.updateFoodReserve(reserveFood);
        resourceManager.updateFood(playerFood);
        resourceManager.updateEnergy(playerEnergy);
        resourceManager.updateOxygen(playerOxygen);
        resourceManager.updateScrapReserve(reserveScrap);
        resourceManager.updateShipHealth(GetShipHealth());
    }

    public void ConsumeFood()
    {
        foodConsumptionTimeTracker += Time.deltaTime;

        if (foodConsumptionTimeTracker >= foodConsumptionPeriod)
        {
            foodConsumptionTimeTracker = foodConsumptionTimeTracker % foodConsumptionPeriod;
            
            if (playerFood <= 0)
                player.Die();
            else
                playerFood = playerFood - foodConsumptionRate;
        }
    }

    public void ConsumeOxygen()
    {
        oxygenConsumptionTimeTracker += Time.deltaTime;

        if (oxygenConsumptionTimeTracker >= oxygenConsumptionPeriod)
        {
            oxygenConsumptionTimeTracker = oxygenConsumptionTimeTracker % oxygenConsumptionPeriod;
            if (playerOxygen <= 0)
                player.Die();
            else
                playerOxygen = playerOxygen - oxygenConsumptionRate;
        }
    }

    public void ConsumeEnergy()
    {
        energyConsumptionTimeTracker += Time.deltaTime;

        if (energyConsumptionTimeTracker >= energyConsumptionPeriod)
        {
            energyConsumptionTimeTracker = energyConsumptionTimeTracker % energyConsumptionPeriod;
            //if (playerEnergy <= 0)
                //Debug.Log("You Died! -- No Energy :(");
            //else
            playerEnergy = playerEnergy - energyConsumptionRate;
        }
    }


    public void PauseFoodConsumption()
    {
        consumeFood = false;
    }

    public void ResumeFoodConsumption()
    {
        consumeFood = true;
        foodConsumptionTimeTracker = 0f;
    }

    public void PauseOxygenConsumption()
    {
        consumeOxygen = false;
    }

    public void ResumeOxygenConsumption()
    {
        consumeOxygen = true;
        oxygenConsumptionTimeTracker = 0f;
    }

    public void PauseEnergyConsumption()
    {
        consumeEnergy = false;
    }

    public void ResumeEnergyConsumption()
    {
        consumeEnergy = true;
        energyConsumptionTimeTracker = 0f;
    }

    public void ResetOxygen()
    {
        Utils.spawnAudio(player.gameObject, refillOxygenClip, 0.35f);
        playerOxygen = GetMaxOxygen();
    }

    public void ResetEnergy()
    {
        playerEnergy = maxPlayerEnergy;
    }

    public void AddEnergy(float energy)
    {
        playerEnergy = Mathf.Min(playerEnergy + energy, maxPlayerEnergy);

    }

    public void AddFood(float food)
    {
        playerFood = Mathf.Min(playerFood + food, maxPlayerFood);
    }
        
    public void RefillFood()
    {
        float consumeAmount = Mathf.Min(maxPlayerFood - playerFood, reserveFood * foodConsumptionRatio);
        reserveFood = reserveFood - consumeAmount / foodConsumptionRatio;
        AddFood(consumeAmount);        
    }

    public float GetShipHealth()
    {
        return ship.GetHealth();
    }

    public void RestoreShipHealth()
    {
        ship.RestoreHull();
    }  

    public float GetPlayerOxygen()
    {
        return playerOxygen;
    }

    public float GetPlayerEnergy()
    {
        return playerEnergy;
    }

    public float GetPlayerFood()
    {
        return playerFood;
    }

    public float GetReserveFood()
    {
        return reserveFood;
    }

    public float GetReserveScrap()
    {
        return reserveScrap;
    }

    public void RemoveReserveScrap(float amount)
    {
       reserveScrap -= amount;
        if (reserveScrap < 0)
        {
            reserveScrap = 0;
        }
    }

    public void ProcessNutrients()
    {
        int nurition = player.GetInventory().CountItemsByType(Item.ItemType.Nurition);
        reserveFood = reserveFood + (nurition * foodProcessingLvlMultiplier[foodProcessingLvl-1]);        
        player.GetInventory().RemoveAllItemsByType(Item.ItemType.Nurition);
        
    }

    public void DepositScrap()
    {        
        reserveScrap = reserveScrap + (player.GetInventory().CountItemsByType(Item.ItemType.Scrap));        
        player.GetInventory().RemoveAllItemsByType(Item.ItemType.Scrap);
    }

    public void Win()
    {

    }    
}
