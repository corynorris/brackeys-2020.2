using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour, IShipStationMenu
{
    [SerializeField]
    private LevelController lvlController;

    [SerializeField]
    private Button bootsButton;
    [SerializeField]
    private Button batteryButton;
    [SerializeField]
    private Button tankButton;

    [SerializeField]
    private Text bootsAmountText;

    [SerializeField]
    private Text batteryAmountText;

    [SerializeField]
    private Text tankAmountText;

    private float batteryCost;
    private float tankCost;
    private float bootsCost;

    // Start is called before the first frame update
    void Start()
    {
        lvlController = LevelController.GetInstance().GetComponent<LevelController>();
        checkButtonStatus();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CraftBoots()
    {

        if (bootsCost <= lvlController.GetReserveScrap())
        {
            //add boots?
            lvlController.RemoveReserveScrap(bootsCost);
        }
        checkButtonStatus();
    }

    public void CraftBattery()
    {

        if (batteryCost <= lvlController.GetReserveScrap())
        {
            //add battery?
            lvlController.RemoveReserveScrap(batteryCost);
        }
        checkButtonStatus();
    }

    public void CraftTank()
    {

        if (tankCost <= lvlController.GetReserveScrap())
        {
            //add battery?
            lvlController.RemoveReserveScrap(tankCost);
        }
        checkButtonStatus();
    }

    public void checkButtonStatus()
    {
        if (bootsButton)
        {
            if (bootsCost <= lvlController.GetReserveScrap())//cost check
            {
                batteryButton.enabled = true;
            }
            else
            {
                batteryButton.enabled = false;
            }
        }

        if (batteryButton)
        {
            if (batteryCost <= lvlController.GetReserveScrap())//cost check
            {
                bootsButton.enabled = true;
            }
            else
            {
                bootsButton.enabled = false;
            }
        }

        if (tankButton)
        {
            if (tankCost <= lvlController.GetReserveScrap())//cost check
            {
                tankButton.enabled = true;
            }
            else
            {
                tankButton.enabled = false;
            }
        }

        //setBootsAmountText("Have: " );
        setBatteryAmountText("Have: " );
        setTankAmountText("Have: "  );
    }

    private void setBootsAmountText(string text)
    {
        bootsAmountText.text = text;
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
