using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour, IShipStationMenu
{
    private LevelController lvlController;

  
    [SerializeField]
    private Button batteryButton;
    [SerializeField]
    private Button tankButton;

  

    [SerializeField]
    private Text batteryAmountText;

    [SerializeField]
    private Text tankAmountText;
 

    // Start is called before the first frame update
    void Start()
    {
        //checkButtonStatus();
        lvlController = FindObjectOfType<LevelController>();
        Render();

    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    private void Render()
    {
        setBatteryAmountText("Crafting Cost: " + lvlController.GetBatteryPackCost() + " Scrap");
        setTankAmountText("Crafting Cost: " + lvlController.GetOxygenTankCost() + " Scrap");
        checkButtonStatus();
    }

    public void CraftBattery()
    {        
        if (lvlController.GetBatteryPackCost() <= lvlController.GetReserveScrap())
        {
            //add battery?
            lvlController.RemoveReserveScrap(lvlController.GetBatteryPackCost());
        }
        Render();
    }

    public void CraftTank()
    {

        if (lvlController.GetOxygenTankCost() <= lvlController.GetReserveScrap())
        {
            //add battery?
            lvlController.RemoveReserveScrap(lvlController.GetOxygenTankCost());
        }
        Render();
    }

    public void checkButtonStatus()
    {
       // TODO: check if there is inventory space 

        if (batteryButton)
        {
            if (lvlController.GetBatteryPackCost() <= lvlController.GetReserveScrap())//cost check
            {
                batteryButton.enabled = true;
            }
            else
            {
                batteryButton.enabled = false;
            }
        }

        if (tankButton)
        {
            if (lvlController.GetOxygenTankCost() <= lvlController.GetReserveScrap())//cost check
            {
                tankButton.enabled = true;
            }
            else
            {
                tankButton.enabled = false;
            }
        }        
    }

    private void setBatteryAmountText(string text)
    {
        batteryAmountText.text = text;
    }

    private void setTankAmountText(string text)
    {
        tankAmountText.text = text;
    }

}
