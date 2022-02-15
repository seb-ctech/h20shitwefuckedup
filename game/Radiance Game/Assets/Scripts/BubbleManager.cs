using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleManager : MonoBehaviour {

    public ParticleSystem[] particleSystems;
    //public float defButtonStrength = 0.5f;

    private ButtonEvent button;
    private GameObject buttonHandler;

    // Start is called before the first frame update
    void Start()
    {
        InitializeButtonControls();
        for (int i=0; i<particleSystems.Length; i++){
            particleSystems[i].Stop();
        }

    }

    void InitializeButtonControls()
    {
        buttonHandler = GameObject.Find("ButtonHandler");
        button = buttonHandler.GetComponent<ButtonEventDispatcher>().GetEvent();
        button.AddListener(OnButtonPush);
    }

    void OnButtonPush(int index, float value)

    {
        Debug.Log(value);
        StartParticles(index,value);
    }

    void StartParticles(int index,float buttonStrength)
    {
        ParticleSystem particle = particleSystems[index];
        var emission = particle.emission;
        emission.rateOverTime = buttonStrength*20.0f;
        Debug.Log("Start Bubbles");
        particle.Play();

        float waterLevel = 20.0f; //here: get water level
        var main = particle.main;
        main.startLifetime = Random.Range(waterLevel * 0.1f, waterLevel * 0.175f);
    }
}
