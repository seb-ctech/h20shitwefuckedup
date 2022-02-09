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
    private float delta;
    private bool zn_1, zn_2, zn_3, zn_4;

    private float upForce = 0.2f;
    // Start is called before the first frame update
    public float noiseFactor;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        delta = 0.0f;
        buttonPress_up = false;
        buttonPress_left = false;
        buttonPress_right = false;
        buttonPress_down = false;

    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKey("up"))
    //     {
    //         rb.AddForce(Vector3.Scale(Vector3.up, new Vector3(0, 2, 0)));
    //         AddNoiseOffset();
    //     }

    //     if (Input.GetKey("right"))
    //     {
    //         rb.drag = 20;
    //     }
    //     else
    //     {
    //         rb.drag = 0;
    //     }

    //     if (Input.GetKey("left"))
    //     {
    //         rb.AddTorque(Vector3.right * 0.1f);
    //     }
    //     delta += 10.0f;
    // }


    void Update()
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

        delta += 1.0f;
    }
    void FixedUpdate()
    {

        if (buttonPress_up == true && zn_1)
        {

            rb.AddForce(0, upForce, 0, ForceMode.Impulse);
            buttonPress_up = false;

        }
        else if (buttonPress_left && zn_2)
        {
            rb.AddForce(0, upForce, 0, ForceMode.Impulse);
            buttonPress_left = false;
        }
        else if (buttonPress_right && zn_3)
        {
            rb.AddForce(0, upForce, 0, ForceMode.Impulse);
            buttonPress_right = false;
        }
        else if (buttonPress_down && zn_4)
        {
            rb.AddForce(0, upForce, 0, ForceMode.Impulse);
            buttonPress_down = false;
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

    // void AddNoiseOffset()
    // {
    //     Vector3 noise = new Vector3(
    //                 (Mathf.PerlinNoise(delta, delta - 5.0f) * 2.0f - 1.0f),
    //                 (Mathf.PerlinNoise(delta - 20f, delta + 3.0f) * 2.0f - 1.0f),
    //                 (Mathf.PerlinNoise(delta + 2.0f, delta - 3.0f) * 2.0f - 1.0f));
    //     rb.AddForce(noise);
    //     Debug.Log("Noise: " + noise);
    //     Debug.Log("Velocity: " + rb.velocity);
    // }

}

