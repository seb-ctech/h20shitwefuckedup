using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZoneControl {
    private int index;
    private bool inside;
    private bool setNewNoise;
    private Vector3 noiseDirection;
    
    public ZoneControl(int _index){
        index = _index;
        inside = false;
        setNewNoise = false;
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

    public bool ShouldRequestNewNoise(){
        return setNewNoise;
    }

    public void ResetNoise(){
        setNewNoise = false;
    }

    public Vector3 GetNoiseDirection(){
        return noiseDirection;
    }

    public void RequestNewNoise(){
        setNewNoise = true;
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
    private float upForce = 5.0f;

    private ZoneControl[] zones;
    private ButtonEvent button;
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
        float buttonUpForce = 100.0f;
        if (zones[index].IsInZone())
        {
            if (value < 0 || value > 0)
            {
                upForce_strength_noise = Random.Range(-30.0f, 80.0f);
            }
            rb.AddForce(upForce_strength_noise, buttonUpForce * value, upForce_strength_noise);
            rb.AddTorque(1.0f, upForce_strength_noise / 6, 4.0f);
        }
        AddNoiseDisplacement(index);
    }

    void InitializeButtonControls()
    {
        buttonHandler = GameObject.Find("ButtonHandler");
        button = buttonHandler.GetComponent<ButtonEventDispatcher>().GetEvent();
        button.AddListener(OnButtonPush);
    }



    void AddNoiseDisplacement(int index)
    {
        if (zones[index].IsInZone())
        {
            if (zones[index].ShouldRequestNewNoise())
            {
                upForce_strength_noise = Random.Range(minNoise, maxNoise);
                zones[index].ResetNoise();
            }
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
            Debug.Log("Collision exit " + zone.gameObject.name);
            zones[0].SetInside(false);
            zones[0].RequestNewNoise();
        }
        else if (zone.gameObject.name == "Zone_2")
        {
            zones[1].SetInside(false);
            zones[1].RequestNewNoise();
        }
        else if (zone.gameObject.name == "Zone_3")
        {
            zones[2].SetInside(false);
            zones[2].RequestNewNoise();
        }
        else if (zone.gameObject.name == "Zone_4")
        {
            zones[3].SetInside(false);
            zones[3].RequestNewNoise();
        }
    }

}

