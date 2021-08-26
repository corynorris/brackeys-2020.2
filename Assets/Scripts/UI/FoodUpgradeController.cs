using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodUpgradeController : MonoBehaviour, IShipStationMenu
{    
    private LevelController lvlController;

    [SerializeField]
    private GameObject upgradeButton;

    [SerializeField]
    private Text currLevelText;
    [SerializeField]
    private Text nextLevelText;

    [SerializeField]
    private Text nextLevelCost;

    [SerializeField]
    private Text currLevelDesc;
    [SerializeField]
    private Text nextLevelDesc;

        
  
    
    [SerializeField] GameObject upgradeErrorNotification;

    // Start is called before the first frame update
    void Start()
    {
        lvlController = FindObjectOfType<LevelController>();
        Render();       
        
        //UpgradeFood();
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    private void Render()
    {
        setCurrLevelText("Level: " + lvlController.foodProcessingLvl);
        setCurrLevelDesc("One nutrient unit processed into " + lvlController.GetFoodProcessingLvlMultiplier()[lvlController.foodProcessingLvl - 1] + " food units.");
        


        if (lvlController.NextFoodProcessingLvlCost() > 0)
        {
            setNextLevelText("Level: " + (lvlController.foodProcessingLvl + 1));
            setNextLevelDesc("One nutrient unit processed into " + lvlController.GetFoodProcessingLvlMultiplier()[lvlController.foodProcessingLvl] + " food units.");
            setNextLevelCost("Upgrade Cost: " + lvlController.NextFoodProcessingLvlCost() + " Scrap");
        }
        else
        {
            setNextLevelText("N/A");
            setNextLevelDesc("Maximum efficiency reached.");
            setNextLevelCost("Maximum efficiency reached.");
        }

        if(lvlController.GetReserveScrap() >= lvlController.NextFoodProcessingLvlCost())
        {
            upgradeButton.SetActive(true);
            upgradeErrorNotification.SetActive(false);
        }
        else
        {
            upgradeButton.SetActive(false);
            upgradeErrorNotification.SetActive(true);
        }




    }

    public void UpgradeFood()
    {       
        if (lvlController.NextFoodProcessingLvlCost() <= lvlController.GetReserveScrap())//cost check
        {
            lvlController.RemoveReserveScrap(lvlController.NextFoodProcessingLvlCost());
            lvlController.foodProcessingLvl++;
            Render();
        }
       
    }

    private void setCurrLevelText(string text)
    {
        currLevelText.text = text;
    }

    private void setNextLevelText(string text)
    {
        nextLevelText.text = text;
    }

    private void setCurrLevelDesc(string text)
    {
        currLevelDesc.text = text;
    }

    private void setNextLevelDesc(string text)
    {
        nextLevelDesc.text = text;
    }

    private void setNextLevelCost(string cost)
    {
        nextLevelCost.text = cost;
    }

    public void checkButtonStatus()
    {
     
    }
}
