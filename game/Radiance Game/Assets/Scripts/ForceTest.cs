using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTest : MonoBehaviour
{

    //private Variables
    private Rigidbody rb;
    private float delta;
    // Start is called before the first frame update
    public float noiseFactor;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        delta = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            rb.AddForce(Vector3.Scale(Vector3.up, new Vector3(0, 2, 0)));
            AddNoiseOffset();
        }

        if (Input.GetKey("right"))
        {
            rb.drag = 20;
        }
        else
        {
            rb.drag = 0;
        }

        if (Input.GetKey("left"))
        {
            rb.AddTorque(Vector3.right * 0.1f);
        }

        delta += 0.01f;
    }
    void AddNoiseOffset()
    {
        Vector3 noise = new Vector3(
                    (Mathf.PerlinNoise(delta, delta - 5.0f) * 2.0f - 1.0f),
                    (Mathf.PerlinNoise(delta - 20f, delta + 3.0f) * 2.0f - 1.0f),
                    (Mathf.PerlinNoise(delta + 2.0f, delta - 3.0f) * 2.0f - 1.0f));
        rb.AddForce(noise);
        Debug.Log(noise);
    }

}

