using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleManager : MonoBehaviour {

    public Button button1;
    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = button1.GetComponent<Button>();
		btn.onClick.AddListener(StartParticles);
        particle.Stop();
    }

    void StartParticles()
    {
        particle.Play();
    }
}
