using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{

    [SerializeField] Button oxygenButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button batteryButton;
    [SerializeField] Button menuButton;
    [SerializeField] Button soundButton;
    [SerializeField] Button inventoryButton;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (player.GetInventory().HasOxygen())        
            oxygenButton.enabled = true;
        else        
            oxygenButton.enabled = false;
        */
    }

    public void PauseGame()
    {
        Debug.Log("PAUSING THE GAME");
    }

    public void ToggleSound()
    {
        Debug.Log("Toggling Sound");
    }

    public void ToggleMenu()
    {
        Debug.Log("Toggling Menu");
    }
    public void OpenInventory()
    {
        Debug.Log("Opening Inventory");
    }

    public void UseBattery()
    {
        Debug.Log("Using Battery");
    }

    public void UseOxygen()
    {
        //player.UseOxygen();
        Player.Instance.GetInventory().UseItemByType(Item.ItemType.Oxygen);

    }
}
