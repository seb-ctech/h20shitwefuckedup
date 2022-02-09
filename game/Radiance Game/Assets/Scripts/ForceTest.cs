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
    private bool buttonPress_right;
    private float delta;

    private float upForce = 1.0f;
    // Start is called before the first frame update
    public float noiseFactor;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        delta = 0.0f;
        buttonPress_up = false;
        buttonPress_left = false;
        buttonPress_right = false;

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

        delta += 1.0f;
    }
    void FixedUpdate()
    {
        if (buttonPress_up == true)
        {

            rb.AddForce(0, upForce, 0, ForceMode.Impulse);

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

