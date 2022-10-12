using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesMoveY : MonoBehaviour
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

        transform.position = new Vector3(transform.position.x, 12.0f * waterLevel, transform.position.z);

    }
}
