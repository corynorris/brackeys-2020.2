using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{

    private float resourceCount;
        
    [SerializeField]
    private string prefix;
    [SerializeField]
    private string postfix;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateText(float resource)
    {
        gameObject.GetComponent<Text>().text = prefix + " " + resource + " " + postfix;
    }
}
