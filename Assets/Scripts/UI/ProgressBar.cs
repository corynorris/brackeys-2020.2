using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    [SerializeField]
    private GameObject fill;

    public float progress;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateProgressBar();
    }

    public void setProgress(float progress)
    {
        this.progress = progress;
        updateProgressBar();
    }

    private void updateProgressBar()
    {
        this.fill.transform.localScale = new Vector3(this.progress,1,1);
    }

}
