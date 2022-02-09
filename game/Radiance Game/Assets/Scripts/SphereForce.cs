using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereForce : MonoBehaviour
{

    private Rigidbody rb;

    private bool buttonPress_up;
    private bool buttonPress_left;
    private bool buttonPress_right;
    private float sphereHeight;


    private float upForce = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        buttonPress_up = false;
        buttonPress_left = false;
        buttonPress_right = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {


            Invoke("setToTrueUp", 0.6f);



        }



        if (Input.GetKey("left"))
        {
            buttonPress_left = true;
        }

        if (Input.GetKey("right"))
        {
            buttonPress_right = true;
        }

        sphereHeight = GameObject.Find("Sphere").transform.position.y * 0.3f;
        //Debug.Log(GameObject.Find("Sphere").transform.position.y);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * sphereHeight;



    }


    void FixedUpdate()
    {
        if (buttonPress_up == true)
        {

            rb.AddForce(0, upForce, 0, ForceMode.Impulse);
            buttonPress_up = false;

        }


    }

    void setToTrueUp()
    {
        buttonPress_up = true;
    }
}
