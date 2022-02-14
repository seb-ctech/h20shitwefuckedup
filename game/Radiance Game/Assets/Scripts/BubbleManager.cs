using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleManager : MonoBehaviour {

    public Button[] buttons;
    public ParticleSystem[] particleSystems;
    public float buttonStrength = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if(particleSystems.Length == buttons.Length){
            for (int i=0; i<buttons.Length; i++){
                Button btn = buttons[i].GetComponent<Button>();
                ParticleSystem particle = particleSystems[i];
            
                btn.onClick.AddListener(delegate{StartParticles(particle,buttonStrength);});
                particleSystems[i].Stop();
            }
        }
        else{
            Debug.Log("Error: Number of Buttons and Particle Systems do not match");
        }

    }

    void StartParticles(ParticleSystem particle,float buttonStrength)
    {
        var emission = particle.emission;
        emission.rateOverTime = buttonStrength*20.0f;
        particle.Play();

        //TODO: water level immer positiv?
        float waterLevel = 20.0f; //here: get water level
        var main = particle.main;
        main.startLifetime = Random.Range(waterLevel * 0.1f, waterLevel * 0.175f);
    }
}
