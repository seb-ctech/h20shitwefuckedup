using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereForce : MonoBehaviour
{
    private Rigidbody rb;

    private bool zn_1, zn_2, zn_3, zn_4;
    private bool buttonPress_up, buttonPress_left, buttonPress_right, buttonPress_down;
    private float sphereHeight;
    private float sphereDelay = 0.6f;
    private float upForce = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        buttonPress_up = false;
        buttonPress_left = false;
        buttonPress_right = false;
        buttonPress_down = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            Invoke("setToTrueUp", sphereDelay);
        }


        if (Input.GetKey("left"))
        {
            Invoke("setToTrueLeft", sphereDelay);
        }

        if (Input.GetKey("right"))
        {
            Invoke("setToTrueRight", sphereDelay);
        }

        if (Input.GetKey("down"))
        {
            Invoke("setToTrueDown", sphereDelay);
        }

        sphereHeight = transform.position.y * 0.6f;
        //Debug.Log(GameObject.Find("Sphere").transform.position.y);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * sphereHeight;

        // sphereHeight_2 = transform.position.y * 0.3f;
        // //Debug.Log(GameObject.Find("Sphere").transform.position.y);
        // transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * sphereHeight_2;

        // sphereHeight_3 = transform.position.y * 0.3f;
        // //Debug.Log(GameObject.Find("Sphere").transform.position.y);
        // transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * sphereHeight_3;

        // sphereHeight_4 = transform.position.y * 0.3f;
        // //Debug.Log(GameObject.Find("Sphere").transform.position.y);
        // transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * sphereHeight_4;

        // sphereHeight_5 = transform.position.y * 0.3f;
        // //Debug.Log(GameObject.Find("Sphere").transform.position.y);
        // transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * sphereHeight_5;
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




    void setToTrueUp()
    {
        buttonPress_up = true;
    }

    void setToTrueLeft()
    {
        buttonPress_left = true;
    }

    void setToTrueRight()
    {
        buttonPress_right = true;
    }

    void setToTrueDown()
    {
        buttonPress_down = true;
    }
}
