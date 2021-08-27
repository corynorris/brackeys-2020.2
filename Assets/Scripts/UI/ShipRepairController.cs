using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipRepairController : MonoBehaviour, IShipStationMenu
{
    [SerializeField]
    private LevelController lvlController;

    [SerializeField]
    private Button repairButton;


    [SerializeField]
    private Button part1Button;
    [SerializeField]
    private Button part2Button;
    [SerializeField]
    private Button part3Button;
    [SerializeField]
    private Button part4Button;

    [SerializeField]
    private Text repairText;
    [SerializeField]
    private Text hullRepairCost;

    [SerializeField]
    private Text part1Text;
    [SerializeField]
    private Text part2Text;
    [SerializeField]
    private Text part3Text;
    [SerializeField]
    private Text part4Text;

    [SerializeField]
    private Text part1Cost;
    [SerializeField]
    private Text part2Cost;
    [SerializeField]
    private Text part3Cost;
    [SerializeField]
    private Text part4Cost;

    private float part1RepairCost;
    private float part2RepairCost;
    private float part3RepairCost;
    private float part4RepairCost;

    [SerializeField]
    private string[] repairCostArray;

    [SerializeField]
    private string[] partNamesArray;


    // Start is called before the first frame update
    void Start()
    {
        lvlController = FindObjectOfType<LevelController>();
        Render();
        checkButtonStatus();
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }    

    private void Render()
    {
        part1Text.text = partNamesArray[0];
        part2Text.text = partNamesArray[1];
        part3Text.text = partNamesArray[2];
        part4Text.text = partNamesArray[3];
        
        if(!lvlController.GetShip().ReactorStatus())
            part1Cost.text = "Repair Cost " + lvlController.GetShip().GetReactorRepairCost();
        else
        {
            part1Cost.text = "Reactor has been fully restored!";
            part1Button.enabled = false;
        }

        if (!lvlController.GetShip().ThrustersStatus())
            part2Cost.text = "Repair Cost " + lvlController.GetShip().GetThrustersRepairCost();
        else
        {
            part2Cost.text = "Thrusters have been fully restored!";
            part2Button.enabled = false;
        }

        if (!lvlController.GetShip().CockpitStatus())
            part3Cost.text = "Repair Cost " + lvlController.GetShip().GetCockpitRepairCost();
        else
        {
            part3Cost.text = "Cockpit has been fully restored!";
            part3Button.enabled = false;
        }

        if (!lvlController.GetShip().WingStatus())
            part4Cost.text = "Repair Cost " + lvlController.GetShip().GetWingRepairCost();
        else
        {
            part4Cost.text = "Wings have been fully restored!";
            part4Button.enabled = false;
        }
        
        hullRepairCost.text = "Repair Cost " + lvlController.GetShip().GetHullRepairCost();
        setRepairText(Mathf.RoundToInt(lvlController.GetShipHealth()) + "%");
    }

    public void repair()
    {

        if (lvlController.GetShip().GetHullRepairCost() <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(lvlController.GetShip().GetHullRepairCost());
            lvlController.RestoreShipHealthMax();
        }
        checkButtonStatus();
    }

    public void repairPart1()
    {
        if (lvlController.GetShip().GetReactorRepairCost() <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(lvlController.GetShip().GetReactorRepairCost());
            lvlController.GetShip().RestoreReactorHealthMax();
            //repair part
        }
        checkButtonStatus();
    }

    public void repairPart2()
    {
        if (lvlController.GetShip().GetThrustersRepairCost() <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(lvlController.GetShip().GetThrustersRepairCost());
            lvlController.GetShip().RestoreThrustersHealthMax();
            //repair part
        }
        checkButtonStatus();
    }

    public void repairPart3()
    {
        if (lvlController.GetShip().GetCockpitRepairCost() <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(lvlController.GetShip().GetCockpitRepairCost());
            lvlController.GetShip().RestoreCockpitHealthMax();
            //repair part
        }
        checkButtonStatus();
    }

    public void repairPart4()
    {
        if (lvlController.GetShip().GetWingRepairCost() <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(lvlController.GetShip().GetWingRepairCost());
            lvlController.GetShip().RestoreWingsHealthMax();
            //repair part
        }
        checkButtonStatus();
    }

    public void checkButtonStatus()
    {
        if (lvlController.GetShip().GetHullRepairCost() <= lvlController.GetReserveScrap())//cost check

        {
            repairButton.enabled = true;
        }
        else
        {
            repairButton.enabled = false;
        }        

        if (true){//part 1 check
            part1Button.enabled = true;
        }
        else
        {
            part1Button.enabled = false;
            part1Text.text = "Ship Part1 fixed";
            //part1Cost.SetActive(false);
        }
        if (true)
        {//part 2 check
            part2Button.enabled = true;
        }
        else
        {
            part2Button.enabled = false;
            part2Text.text = "Ship Part2 fixed";
            //part2Cost.SetActive(false);
        }
        if (true)
        {//part 3 check
            part3Button.enabled = true;
        }
        else
        {
            part3Button.enabled = false;
            part3Text.text = "Ship Part3 fixed";
            //part3Cost.SetActive(false);
        }
        if (true)
        {//part 4 check
            part4Button.enabled = true;
        }
        else
        {
            part4Button.enabled = false;
            part4Text.text = "Ship Part4 fixed";
            //part4Cost.SetActive(false);
        }
    }

    private void setRepairText(string text)
    {
        repairText.text = text;
    }

}
