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

    private Player player;
  

    [SerializeField]
    private Text batteryAmountText;

    [SerializeField]
    private Text tankAmountText;

    [SerializeField]
    private AudioClip craftItem;

    // Start is called before the first frame update
    void Start()
    {
        //checkButtonStatus();
        lvlController = FindObjectOfType<LevelController>();
        Render();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    private void Render()
    {
        setBatteryAmountText("Crafting Cost " + lvlController.GetBatteryPackCost() + " Scrap");
        setTankAmountText("Crafting Cost " + lvlController.GetOxygenTankCost() + " Scrap");
        checkButtonStatus();
    }

    public void CraftBattery()
    {        
        if (lvlController.GetBatteryPackCost() <= lvlController.GetReserveScrap())
        {
            //add battery?
            lvlController.RemoveReserveScrap(lvlController.GetBatteryPackCost());
            Utils.spawnAudio(player.gameObject, craftItem, 0.4f);
        }
        Render();
    }

    public void CraftTank()
    {

        if (lvlController.GetOxygenTankCost() <= lvlController.GetReserveScrap())
        {
            //add battery?
            lvlController.RemoveReserveScrap(lvlController.GetOxygenTankCost());
            player.GetInventory().AddItem(new Item { amount = 1, itemType = Item.ItemType.Oxygen });
            Utils.spawnAudio(player.gameObject, craftItem, 0.4f);
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
