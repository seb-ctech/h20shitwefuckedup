using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    [Range(1.0f, 100f)]
    public float releaseRate = 0.001f;

    private float waterLevel = 1f;
    private float waterLeak = 0f;
    private float gravity = 0.5f;
    private Transform waterSurfaceT;

    // Start is called before the first frame update
    void Start()
    {
        waterSurfaceT = GameObject.Find("WaterSurface").transform;
        waterLevel = 1.0f;
        waterLeak = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        SetSurfacePositionByLevel(waterLevel);
        TestWaterLevelOverTime();
    }

    public void LeakWater()
    {
        float rate = releaseRate / 10000f;
        waterLeak = Mathf.Clamp(waterLeak + rate, 0.0f, 1.0f);
        waterLevel = Mathf.Clamp(waterLevel - rate, 0.0f, 1.0f);
    }
    
    void TestWaterLevelOverTime(){
        if(Time.frameCount % 30 == 0)
        {
            LeakWater();
        }
    }

    void SetSurfacePositionByLevel(float level)
    {
        float height = level - 0.5f;
        Vector3 position = waterSurfaceT.localPosition;
        position.y = height;
        waterSurfaceT.localPosition = position;
    }


    float GetWaterLevel()
    {
        return waterLevel;
    }
}

