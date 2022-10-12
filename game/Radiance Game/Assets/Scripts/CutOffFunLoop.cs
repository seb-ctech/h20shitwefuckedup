using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOffFunLoop : MonoBehaviour
{

    private WaterLevel wl;
    // Start is called before the first frame update
    void Start()
    {
        wl = GameObject.Find("WaterTank").GetComponent<WaterLevel>();

    }

    // Update is called once per frame
    void Update()
    {
        float waterLevel = wl.GetWaterLevel();

        GetComponent<AudioLowPassFilter>().cutoffFrequency = (1000 * waterLevel);

    }
}
