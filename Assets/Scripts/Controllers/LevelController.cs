using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] Ship ship;

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


    // Start is called before the first frame update
    void Start()
    {
        resourceManager = ResourceManager.GetInstance();
        ResumeFoodConsumption();
        ResumeOxygenConsumption();
        ResumeEnergyConsumption();
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
                Debug.Log("You Died! -- No Food :(");
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
                Debug.Log("You Died! -- No O2 :(");
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
            if (playerEnergy <= 0)
                Debug.Log("You Died! -- No Energy :(");
            else
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
        playerOxygen = maxPlayerOxygen;
    }

    public void ResetEnergy()
    {
        playerEnergy = maxPlayerEnergy;
    }

    public void AddEnergy(float energy)
    {
        playerEnergy = Mathf.Max(playerEnergy + energy, maxPlayerEnergy);
    }

    public void AddFood(float food)
    {
        playerFood = Mathf.Max(playerFood + food, maxPlayerFood);
    }
    public void RefillFood()
    {
        float consumeAmount = Mathf.Min(maxPlayerFood - playerFood, reserveFood);
        reserveFood = reserveFood - consumeAmount;
        AddFood(consumeAmount);        
    }

    public float GetShipHealth()
    {
        return ship.GetHealth();
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
}
