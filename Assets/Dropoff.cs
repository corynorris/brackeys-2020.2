using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropoff : MonoBehaviour
{

    LevelController levelControler;

    // Start is called before the first frame update
    void Start()
    {
        levelControler = FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        levelControler.PauseEnergyConsumption();
        levelControler.ResetOxygen();
        levelControler.PauseFoodConsumption();
        levelControler.PauseOxygenConsumption();
    }

    void OnTriggerExit2D(Collider2D otherObject)
    {
        levelControler.ResumeEnergyConsumption();
        levelControler.ResumeFoodConsumption();
        levelControler.ResumeOxygenConsumption();
    }

}
