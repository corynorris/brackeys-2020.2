using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStationController : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    public bool isExit;

    [SerializeField]
    public bool isEntrance;

    private Player player;

    [SerializeField]
    private GameObject highlight;

    [SerializeField]
    private AudioClip activateSound;

    [SerializeField]
    private AudioClip deactivateSound;

    [SerializeField]
    private AudioClip openDoor;

    [SerializeField]
    private AudioClip closeDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOnMenu(Player player)
    {
        Utils.spawnAudio(gameObject, activateSound, 0.35f);
        Debug.Log("TURNING ON MENU");
        this.player = player;
        menu.SetActive(true);
    }

    public void turnOffMenu()
    {
        Utils.spawnAudio(gameObject, deactivateSound, 0.25f);
        menu.SetActive(false);
    }

    public void highlightStation(bool turnOn)
    {
        highlight.SetActive(turnOn);
        if(isEntrance || isExit)
        {
            if (turnOn)
            {
                Utils.spawnAudio(gameObject, openDoor, 0.4f);
            }
            else
            {
                Utils.spawnAudio(gameObject, closeDoor, 0.4f);
            }
        }
    }

    public void turnOffPlayerMenu()
    {
        player.CloseMenu();
    }
}
