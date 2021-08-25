using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodUpgradeController : MonoBehaviour, IShipStationMenu
{
    [SerializeField]
    private LevelController lvlController;

    [SerializeField]
    private Button upgradeButton;

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



    // Start is called before the first frame update
    void Start()
    {
        UpgradeFood();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeFood()
    {
        
        if (lvlController.foodProcessingLvl < lvlController.foodProcessingCost.Length)
        {
            if (lvlController.foodProcessingCost[lvlController.foodProcessingLvl] <= lvlController.GetReserveScrap())//cost check
            {
                setCurrLevelText("Level: " + lvlController.foodProcessingLvl + 1);
                setNextLevelText("Level: " + lvlController.foodProcessingLvl + 2);
                setCurrLevelDesc(lvlController.foodProcessingDesc[lvlController.foodProcessingLvl]);
                setNextLevelDesc(lvlController.foodProcessingDesc[lvlController.foodProcessingLvl + 1]);
                lvlController.foodProcessingLvl++;
            }
        }
        else
        {
            setCurrLevelText("Max");
            setNextLevelText("");
            setCurrLevelDesc(lvlController.foodProcessingDesc[lvlController.foodProcessingLvl]);
            setNextLevelDesc("");
            upgradeButton.enabled = false;

        }
    }

    public void checkButtonStatus()
    {
        if (lvlController.foodProcessingCost[lvlController.foodProcessingLvl] <= lvlController.GetReserveScrap())
        {
            upgradeButton.enabled = true;
        }
        else
        {
            upgradeButton.enabled = false;
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
}
