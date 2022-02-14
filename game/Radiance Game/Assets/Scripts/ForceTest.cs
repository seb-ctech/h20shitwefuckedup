using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Refactor as main Torus Physics component and interface with jets
public class ForceTest : MonoBehaviour
{

    //private Variables
    private Rigidbody rb;
    private bool buttonPress_up;
    private bool buttonPress_left;
    private bool buttonPress_right, buttonPress_down;
    private bool zn_1, zn_2, zn_3, zn_4;
    private ButtonEvent button;
    private bool init = false;
    private GameObject buttonHandler;

    private float upForce = 100.0f;
    // Start is called before the first frame update
    public float noiseFactor;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Debug.Log(gameObject.name + " " + rb);
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

            rb.AddForce(0, upForce * value, 0);
        }
    }

    void ControlForceByArrowKeys()
    {
        if (buttonPress_up == true && zn_1)
        {
            rb.AddForce(0, upForce, 0);
            buttonPress_up = false;
        }
        if (buttonPress_left && zn_2)
        {
            rb.AddForce(0, upForce, 0);
            buttonPress_left = false;
        }
        else if (buttonPress_right && zn_3)
        {
            rb.AddForce(0, upForce, 0);
            buttonPress_right = false;
        }
        else if (buttonPress_down && zn_4)
        {
            rb.AddForce(0, upForce, 0);
            buttonPress_down = false;
        }
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
        }
        else if (zone.gameObject.name == "Zone_2")
        {
            zn_2 = false;
        }
        else if (zone.gameObject.name == "Zone_3")
        {
            zn_3 = false;
        }
        else if (zone.gameObject.name == "Zone_4")
        {
            zn_4 = false;
        }
    }

}

