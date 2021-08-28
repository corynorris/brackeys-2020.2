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
        Debug.Log("TURNING ON MENU");
        this.player = player;
        menu.SetActive(true);
    }

    public void turnOffMenu()
    {
        menu.SetActive(false);
    }

    public void highlightStation(bool turnOn)
    {
        highlight.SetActive(turnOn);
    }

    public void turnOffPlayerMenu()
    {
        player.CloseMenu();
    }
}
