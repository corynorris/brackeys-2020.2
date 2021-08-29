using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Overlay : MonoBehaviour
{

    [SerializeField] Button oxygenButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button batteryButton;
    [SerializeField] Button menuButton;
    [SerializeField] Button soundButton;
    [SerializeField] Button inventoryButton;

    private UI_Inventory uiInventory;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        uiInventory = FindObjectOfType<UI_Inventory>();
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
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            //enable player movement
        }
        else
        {
            Time.timeScale = 0;
            //disable player movement
        }
            
    }

    public void ToggleSound()
    {
        AudioListener al = FindObjectOfType<Camera>().GetComponent<AudioListener>();
        al.enabled = !al.enabled;        
    }

    public void ToggleMenu()
    {
        Debug.Log("Toggling Menu");
    }
    public void OpenInventory()
    {
        Debug.Log("Opening Inventory");
        uiInventory.ToggleInventory();
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
