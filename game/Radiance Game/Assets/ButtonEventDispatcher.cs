using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventDispatcher : MonoBehaviour
{
    // Start is called before the first frame update
    private float[] buttonValues;
    void Start()
    {
        buttonValues = new float[8];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ProcessButtonInput(int index, int value){
        float normalize = value / 1000f;
        Debug.Log("Button " + index + " value is: " + normalize);
        buttonValues[index] = normalize;
    }

}
