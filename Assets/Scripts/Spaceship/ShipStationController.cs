using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStationController : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOnMenu()
    {
        menu.SetActive(true);
        menu.GetComponent<IShipStationMenu>().checkButtonStatus();
    }

    public void turnOffMenu()
    {
        menu.SetActive(false);
    }
}
