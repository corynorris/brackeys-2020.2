using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitUpgradeController : MonoBehaviour, IShipStationMenu
{    
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

    [SerializeField]
    private AudioClip upgradeClip;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        lvlController = FindObjectOfType<LevelController>();
        //UpgradeBoots();
        //UpgradeGoggles();
        //UpgradeTank();
        player = FindObjectOfType<Player>();
        Render();

    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    private void Render()
    {
        setTankLevelText("Level " + lvlController.suitTankLvl);
        setBootsLevelText("Level " + lvlController.suitBootsLvl);
        setGogglesLevelText("Level " + lvlController.suitGogglesLvl);

        if (lvlController.NextSuiteTankLvlCost() > 0)
        {
            setTankCostText("Upgrade Cost " + lvlController.NextSuiteTankLvlCost() + " Scrap");            
            if (lvlController.GetReserveScrap() < lvlController.NextSuiteTankLvlCost())
                tankButton.enabled = false;         
            
        }
        else
        {
            setTankCostText("Maximum efficiency reached.");
            tankButton.enabled = false;
        }

        if (lvlController.NextSuiteGogglesLvlCost() > 0)
        {
            setGogglesCostText("Upgrade Cost " + lvlController.NextSuiteGogglesLvlCost() + " Scrap");
            if (lvlController.GetReserveScrap() < lvlController.NextSuiteGogglesLvlCost())
                gogglesButton.enabled = false;

        }
        else
        {
            setGogglesCostText("Maximum efficiency reached.");
            gogglesButton.enabled = false;
        }

        if (lvlController.NextSuiteBootsLvlCost() > 0)
        {
            setBootsCostText("Upgrade Cost " + lvlController.NextSuiteBootsLvlCost() + " Scrap");
            if (lvlController.GetReserveScrap() < lvlController.NextSuiteBootsLvlCost())
                bootsButton.enabled = false;

        }
        else
        {
            setBootsCostText("Maximum efficiency reached.");
            bootsButton.enabled = false;
        }


    }

    public void UpgradeBoots()
    {

        if (lvlController.NextSuiteBootsLvlCost() > 0)
        {
            lvlController.RemoveReserveScrap(lvlController.NextSuiteBootsLvlCost());
            lvlController.suitBootsLvl++;
            Utils.spawnAudio(player.gameObject, upgradeClip, 0.65f);
            Render();
        }
    }

    public void UpgradeGoggles()
    {

        if (lvlController.NextSuiteGogglesLvlCost() > 0)
        {
            lvlController.RemoveReserveScrap(lvlController.NextSuiteGogglesLvlCost());
            lvlController.suitGogglesLvl++;
            Utils.spawnAudio(player.gameObject, upgradeClip, 0.65f);
            Render();
        }
    }

    public void UpgradeTank()
    {       
        if (lvlController.NextSuiteTankLvlCost() > 0)
        {            
            lvlController.RemoveReserveScrap(lvlController.NextSuiteTankLvlCost());
            lvlController.suitTankLvl++;
            Utils.spawnAudio(player.gameObject, upgradeClip, 0.65f);
            Render();
        }
    }

    public void checkButtonStatus()
    {
        /*
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
        /*
        if (lvlController.suitTankCost[lvlController.suitTankLvl] <= lvlController.GetReserveScrap())//cost check
        {
            tankButton.enabled = true;
        }
        else
        {
            tankButton.enabled = false;
        }*/
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
