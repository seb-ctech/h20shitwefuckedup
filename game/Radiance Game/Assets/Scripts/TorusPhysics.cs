using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZoneControl {
    private int index;
    private bool inside;
    private Vector3 noiseDirection;
    private Vector3 noise;
    
    public ZoneControl(int _index){
        index = _index;
        inside = false;
    }

    public void SetInside(bool isInside){
        inside = isInside;
    }

    public void SetNoiseDirection(Vector3 direction){
        noiseDirection = direction;
    }

    public bool IsInZone(){
        return inside;
    }

    public Vector3 GetNoiseDirection(){
        return noiseDirection;
    }

    public void MakeNewNoise(float min, float max){
        noise = new Vector3(Random.Range(min, max), 1.0f, Random.Range(min, max));
    }

    public Vector3 GetNoise(){
        return noise;
    }


}

//TODO: Refactor as main Torus Physics component and interface with jets
public class TorusPhysics : MonoBehaviour
{

    //private Variables
    private Rigidbody rb;
    
    private bool init = false;
    private float upForce_strength_noise;

    [Range(-10.0f, 50.0f)]
    public float minNoise, maxNoise;
    private float upForce = 2.0f;

    private ZoneControl[] zones;
    private ButtonEvent button;
    private ButtonEventEdge buttonEdge;
    private GameObject buttonHandler;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        CreateZones();
    }

    void CreateZones(){
        zones = new ZoneControl[4];
        for(int i = 0; i < zones.Length; i++){
            zones[i] = new ZoneControl(i);
        }
        zones[0].SetNoiseDirection(new Vector3(1.0f, 0.0f, -1.0f));
        zones[1].SetNoiseDirection(new Vector3(-1.0f, 0.0f, -1.0f));
        zones[2].SetNoiseDirection(new Vector3(1.0f, 0.0f, 1.0f));
        zones[3].SetNoiseDirection(new Vector3(-1.0f, 0.0f, 1.0f));
    }

    void Update()
    {
        if (!init)
        {
            Init();
        }
    }

    void Init()
    {
        InitializeButtonControls();
        init = true;
    }

    void OnButtonPush(int index, float value)
    {
        float buttonUpForce = 120.0f;
        if (zones[index].IsInZone())
        {
            rb.AddForce(zones[index].GetNoise().x, buttonUpForce * value, zones[index].GetNoise().z);
            rb.AddTorque(1.0f, upForce_strength_noise / 6, 4.0f);
        }
        AddNoiseDisplacement(index);
    }

    void OnButtonEdge(int index)
    {
        zones[index].MakeNewNoise(minNoise, maxNoise);
    }

    void InitializeButtonControls()
    {
        buttonHandler = GameObject.Find("ButtonHandler");
        button = buttonHandler.GetComponent<ButtonEventDispatcher>().GetEvent();
        buttonEdge = buttonHandler.GetComponent<ButtonEventDispatcher>().GetEventEdge();
        button.AddListener(OnButtonPush);
        buttonEdge.AddListener(OnButtonEdge);
    }



    void AddNoiseDisplacement(int index)
    {
        if (zones[index].IsInZone())
        {
            rb.AddForce(zones[index].GetNoiseDirection().x * upForce_strength_noise, upForce, zones[index].GetNoiseDirection().z * upForce_strength_noise / 3);
            float toraeHeight = transform.position.y;
            if (toraeHeight > 3)
            {
                rb.AddTorque(1.0f, upForce_strength_noise / 6, 4.0f);
            }
        }
    }

    void OnTriggerEnter(Collider zone)
    {

        if (zone.gameObject.name == "Zone_1")
        {
            zones[0].SetInside(true);
        }
        else if (zone.gameObject.name == "Zone_2")
        {
            zones[1].SetInside(true);
        }
        else if (zone.gameObject.name == "Zone_3")
        {
            zones[2].SetInside(true);
        }
        else if (zone.gameObject.name == "Zone_4")
        {
            zones[3].SetInside(true);
        }
    }

    void OnTriggerExit(Collider zone)
    {
        if (zone.gameObject.name == "Zone_1")
        {
            // Debug.Log("Collision exit " + zone.gameObject.name);
            zones[0].SetInside(false);
        }
        else if (zone.gameObject.name == "Zone_2")
        {
            zones[1].SetInside(false);
        }
        else if (zone.gameObject.name == "Zone_3")
        {
            zones[2].SetInside(false);
        }
        else if (zone.gameObject.name == "Zone_4")
        {
            zones[3].SetInside(false);
        }
    }

}

