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
                //ParticleSystem particle = particleSystems[i];
                int index = i;
                btn.onClick.AddListener(delegate{StartParticles(index,buttonStrength);});
                particleSystems[i].Stop();
            }
        }
        else{
            Debug.Log("Error: Number of Buttons and Particle Systems do not match");
        }

    }

    void StartParticles(int index,float buttonStrength)
    {
        ParticleSystem particle = particleSystems[index];
        var emission = particle.emission;
        emission.rateOverTime = buttonStrength*20.0f;
        particle.Play();

        float waterLevel = 20.0f; //here: get water level
        var main = particle.main;
        main.startLifetime = Random.Range(waterLevel * 0.1f, waterLevel * 0.175f);
    }
}
