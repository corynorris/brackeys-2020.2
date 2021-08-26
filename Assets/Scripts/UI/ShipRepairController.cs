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
        part1Text.text = partNamesArray[1];
        part2Text.text = partNamesArray[2];
        part3Text.text = partNamesArray[3];
        part4Text.text = partNamesArray[4];
        

        part1Cost.text = "Repair Cost " + lvlController.GetShip().GetReactorRepairCost();
        part2Cost.text = "Repair Cost " + lvlController.GetShip().GetThrustersRepairCost();
        part3Cost.text = "Repair Cost " + lvlController.GetShip().GetCockpitRepairCost();
        part4Cost.text = "Repair Cost " + lvlController.GetShip().GetWingRepairCost();
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
        if (part1RepairCost <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(part1RepairCost);
            //repair part
        }
        checkButtonStatus();
    }

    public void repairPart2()
    {
        if (part2RepairCost <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(part2RepairCost);
            //repair part
        }
        checkButtonStatus();
    }

    public void repairPart3()
    {
        if (part3RepairCost <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(part3RepairCost);
            //repair part
        }
        checkButtonStatus();
    }

    public void repairPart4()
    {
        if (part4RepairCost <= lvlController.GetReserveScrap())
        {
            lvlController.RemoveReserveScrap(part4RepairCost);
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
