using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{

    private float resourceCount;

    [SerializeField]
    private GameObject text;
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
        updateText();
    }

    public void addResource(float count)
    {
        this.resourceCount += count;
        updateText();
    }

    private void updateText()
    {
        this.text.GetComponent<Text>().text = prefix + " " + resourceCount + " " + postfix;
    }
}
