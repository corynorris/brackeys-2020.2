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

    [SerializeField]
    private string[] levelIdentifiers;
    [SerializeField]
    private float[] levelCosts;
    [SerializeField]
    private float[] levelEfficiency;

    
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

    }

    private void Render()
    {
        setCurrLevelText("Level: " + levelIdentifiers[lvlController.foodProcessingLvl]);
        setCurrLevelDesc("One nutrient unit processed into " + levelEfficiency[lvlController.foodProcessingLvl] + " food units.");
        


        if (!(lvlController.foodProcessingLvl + 1 >= levelIdentifiers.Length))
        {
            setNextLevelText("Level: " + levelIdentifiers[lvlController.foodProcessingLvl + 1]);
            setNextLevelDesc("One nutrient unit processed into " + levelEfficiency[lvlController.foodProcessingLvl + 1] + " food units.");
            setNextLevelCost("Upgrade Cost: " + levelCosts[lvlController.foodProcessingLvl + 1] + " Scrap.");
        }
        else
        {
            setNextLevelText("N/A");
            setNextLevelDesc("Maximum efficiency reached.");
            setNextLevelCost("Maximum efficiency reached.");
        }

        if(lvlController.GetReserveScrap() >= levelCosts[lvlController.foodProcessingLvl + 1])
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
        if (levelCosts[lvlController.foodProcessingLvl + 1] <= lvlController.GetReserveScrap())//cost check
        {
            lvlController.RemoveReserveScrap(levelCosts[lvlController.foodProcessingLvl + 1]);
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
