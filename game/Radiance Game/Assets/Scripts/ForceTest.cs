using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Refactor as main Torus Physics component and interface with jets
public class ForceTest : MonoBehaviour
{

    //private Variables
    private Rigidbody rb;

    private bool buttonPress_right, buttonPress_down, buttonPress_left, buttonPress_up;
    private bool zn_1, zn_2, zn_3, zn_4;
    private float upForce_strength_noise;
    private bool n_1, n_2, n_3, n_4 = true;
    private bool buttonPushed;
    private ButtonEvent button;
    private bool init = false;
    private GameObject buttonHandler;

    [Range(-10.0f, 50.0f)]
    public float randomNoise_1, randomNoise_2;


    private float buttonUpForce = 100.0f;
    private float upForce = 5.0f;
    // Start is called before the first frame update
    public float noiseFactor;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (!init)
        {
            Init();
        }
        RegisterArrowKeys();
        ControlForceByArrowKeys();
    }

    void Init()
    {
        InitializeButtonControls();
        init = true;
    }

    void RegisterArrowKeys()
    {
        if (Input.GetKey("up"))
        {
            buttonPress_up = true;
        }

        if (Input.GetKey("left"))
        {
            buttonPress_left = true;
        }

        if (Input.GetKey("right"))
        {
            buttonPress_right = true;
        }

        if (Input.GetKey("down"))
        {
            buttonPress_down = true;
        }
    }

    void PushButton(float value)
    {
        if (zn_1)
        {
            if (value < 0 || value > 0 && buttonPushed)
            {
                upForce_strength_noise = Random.Range(-30.0f, 80.0f);

                buttonPushed = false;
            }
            rb.AddForce(upForce_strength_noise, buttonUpForce * value, upForce_strength_noise);
            rb.AddTorque(1.0f, upForce_strength_noise / 6, 4.0f);
        }
    }

    void ControlForceByArrowKeys()
    {
        pressedAnyButton(ref buttonPress_up, zn_1, n_1, 1.0f, -1.0f);
        pressedAnyButton(ref buttonPress_left, zn_2, n_2, -1.0f, -1.0f);
        pressedAnyButton(ref buttonPress_right, zn_3, n_3, 1.0f, 1.0f);
        pressedAnyButton(ref buttonPress_down, zn_4, n_4, -1.0f, 1.0f);
    }

    void InitializeButtonControls()
    {
        buttonPress_up = false;
        buttonPress_left = false;
        buttonPress_right = false;
        buttonPress_down = false;
        buttonHandler = GameObject.Find("ButtonHandler");
        button = buttonHandler.GetComponent<ButtonEventDispatcher>().GetEvent();
        button.AddListener(PushButton);
    }



    void pressedAnyButton(ref bool buttonKey, bool zn, bool noise, float x, float z)
    {
        float toraeHeight;
        if (buttonKey && zn)
        {
            if (noise)
            {
                upForce_strength_noise = Random.Range(randomNoise_1, randomNoise_2);
                noise = false;
            }
            rb.AddForce(x * upForce_strength_noise, upForce, z * upForce_strength_noise / 3);

            toraeHeight = transform.position.y;
            if (toraeHeight > 3)
            {
                rb.AddTorque(1.0f, upForce_strength_noise / 6, 4.0f);
            }
            buttonKey = false;
        }
    }

    void OnTriggerEnter(Collider zone)
    {

        if (zone.gameObject.name == "Zone_1")
        {
            Debug.Log("Collision detetcted hi lucas with" + zone.gameObject.name);
            zn_1 = true;
        }
        else if (zone.gameObject.name == "Zone_2")
        {
            zn_2 = true;
        }
        else if (zone.gameObject.name == "Zone_3")
        {
            zn_3 = true;
        }
        else if (zone.gameObject.name == "Zone_4")
        {
            zn_4 = true;
        }
    }

    void OnTriggerExit(Collider zone)
    {
        if (zone.gameObject.name == "Zone_1")
        {
            Debug.Log("Collision exit " + zone.gameObject.name);
            zn_1 = false;
            buttonPushed = true;
            n_1 = true;
        }
        else if (zone.gameObject.name == "Zone_2")
        {
            zn_2 = false;
            n_2 = true;
        }
        else if (zone.gameObject.name == "Zone_3")
        {
            zn_3 = false;
            n_3 = true;
        }
        else if (zone.gameObject.name == "Zone_4")
        {
            zn_4 = false;
            n_4 = true;
        }
    }

}

