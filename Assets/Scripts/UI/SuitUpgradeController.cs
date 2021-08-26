using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitUpgradeController : MonoBehaviour, IShipStationMenu
{
    [SerializeField]
    private LevelController lvlController;

    [SerializeField]
    private Button bootsButton;
    [SerializeField]
    private Button gogglesButton;
    [SerializeField]
    private Button tankButton;

    [SerializeField]
    private Text bootsLevelText;
    [SerializeField]
    private Text bootsCostText;

    [SerializeField]
    private Text gogglesLevelText;
    [SerializeField]
    private Text gogglesCostText;

    [SerializeField]
    private Text tankLevelText;
    [SerializeField]
    private Text tankCostText;

    // Start is called before the first frame update
    void Start()
    {
        lvlController = FindObjectOfType<LevelController>();
        UpgradeBoots();
        UpgradeGoggles();
        UpgradeTank();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeBoots()
    {
       
        if (lvlController.suitBootsLvl < lvlController.suitBootsCost.Length)
        {
            setBootsLevelText("Level: " + lvlController.suitBootsLvl + 1);
            setBootsCostText("Scrap: " + lvlController.suitBootsCost[lvlController.suitBootsLvl]);
            lvlController.suitBootsLvl++;
        }
        else
        {
            setBootsLevelText("Max");
            setBootsCostText("");
            bootsButton.enabled = false;
        }
    }

    public void UpgradeGoggles()
    {
       
        if (lvlController.suitGogglesLvl < lvlController.suitGogglesCost.Length)
        {
            setGogglesLevelText("Level: " + lvlController.suitGogglesLvl + 1);
            setGogglesCostText("Scrap: " + lvlController.suitGogglesCost[lvlController.suitGogglesLvl]);
            lvlController.suitGogglesLvl++;
            
        }
        else
        {
            setGogglesLevelText("Max");
            setGogglesCostText("");
            gogglesButton.enabled = false;
        }
    }

    public void UpgradeTank()
    {
       
        if (lvlController.suitTankLvl < lvlController.suitTankCost.Length)
        {
            setTankLevelText("Level: " + lvlController.suitTankLvl + 1);
            setTankCostText("Scrap: " + lvlController.suitTankCost[lvlController.suitTankLvl]);
            lvlController.suitTankLvl++;           
        }
        else
        {
            setTankLevelText("Max");
            setTankCostText("");
            tankButton.enabled = false;
        }
    }

    public void checkButtonStatus()
    {
        if (lvlController.suitGogglesCost[lvlController.suitGogglesLvl] <= lvlController.GetReserveScrap())//cost check
        {
            gogglesButton.enabled = true;
        }
        else
        {
            gogglesButton.enabled = false;
        }

        if (lvlController.suitBootsCost[lvlController.suitBootsLvl] <= lvlController.GetReserveScrap())//cost check
        {
            bootsButton.enabled = true;
        }
        else
        {
            bootsButton.enabled = false;
        }

        if (lvlController.suitTankCost[lvlController.suitTankLvl] <= lvlController.GetReserveScrap())//cost check
        {
            tankButton.enabled = true;
        }
        else
        {
            tankButton.enabled = false;
        }
    }

    private void setBootsLevelText(string text)
    {
        bootsLevelText.text = text;
    }

    private void setGogglesLevelText(string text)
    {
        gogglesLevelText.text = text;
    }

    private void setTankLevelText(string text)
    {
        tankLevelText.text = text;
    }

    private void setBootsCostText(string cost)
    {
        bootsCostText.text = cost;
    }

    private void setGogglesCostText(string cost)
    {
        gogglesCostText.text = cost;
    }

    private void setTankCostText(string cost)
    {
        tankCostText.text = cost;
    }
}
