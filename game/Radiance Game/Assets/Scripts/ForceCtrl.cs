using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCtrl : MonoBehaviour
{

    [Range(-9.81f, 2.0f)]
    public float GravityStrength = -5f;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, GravityStrength, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
