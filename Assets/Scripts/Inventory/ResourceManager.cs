using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
        
    
    [SerializeField]
    private GameObject shipHealthSlider;
    [SerializeField]
    private GameObject oxygenSlider;
    [SerializeField]
    private GameObject foodSlider;
    [SerializeField]
    private GameObject energySlider;
    

    [SerializeField]
    private ResourceCounter scrapCounter;
    [SerializeField]
    private ResourceCounter foodCounter;

    private static ResourceManager _instance;
    public static ResourceManager GetInstance() {  return _instance; }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateShipHealth(float shipHealth)
    {
        shipHealthSlider.GetComponentInChildren<Slider>().value = shipHealth;
    }

    public void updateFood(float food)
    {
        
        foodSlider.GetComponentInChildren<Slider>().value = food;

    }

    public void updateOxygen(float oxygen)
    {        
        
        oxygenSlider.GetComponentInChildren<Slider>().value = oxygen;
        
    }

    public void updateEnergy(float energy)
    {        
        
        energySlider.GetComponentInChildren<Slider>().value = energy;
    }

    public void updateScrapReserve(float scrap)
    {        
        this.scrapCounter.updateText(scrap);
    }

    public void updateFoodReserve(float food)
    {     
        this.foodCounter.updateText(food);
    }

}
