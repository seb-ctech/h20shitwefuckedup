using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesSound : MonoBehaviour
{

    [Range(0.1f, 0.3f)]
    public float volumeChangeMultiplier = 0.2f;

    [Range(0.1f, 0.5f)]
    public float pitchChangeMultiplier = 0.2f;

    public AudioClip[] bubbleSounds_4_sek;
    public AudioClip[] bubbleSounds_3_sek;
    public AudioClip[] bubbleSounds_2_sek;
    public AudioClip[] bubbleSounds_1_sek;
    private AudioSource source;
    private ButtonEventEdge button;
    private GameObject buttonHandler;
    private WaterLevel wl;

    private bool init = false;
    // Start is called before the first frame update
    void Start()
    {

        InitializeButtonControls();
        source = gameObject.GetComponent<AudioSource>();

        wl = GameObject.Find("WaterTank").GetComponent<WaterLevel>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S) && wl)
    //     {

    //     }
    // }

    void Update()
    {
        if (!init)
        {
            Init();
        }
    }

    void Init()
    {
        InitializeButtonControls();
        init = true;
    }

    void InitializeButtonControls()
    {
        buttonHandler = GameObject.Find("ButtonHandler");
        button = buttonHandler.GetComponent<ButtonEventDispatcher>().GetEventEdge();
        button.AddListener(OnButtonPush);
    }

    void OnButtonPush(int index)

    {

        float waterLevel = wl.GetWaterLevel();

        if (waterLevel > 0.75f)
        {

            source.clip = bubbleSounds_4_sek[Random.Range(0, bubbleSounds_4_sek.Length)];
        }

        else if (waterLevel < 0.75f && waterLevel > 0.5f)
        {

            source.clip = bubbleSounds_3_sek[Random.Range(0, bubbleSounds_3_sek.Length)];
        }

        else if (waterLevel < 0.5f && waterLevel > 0.25f)
        {

            source.clip = bubbleSounds_2_sek[Random.Range(0, bubbleSounds_2_sek.Length)];
        }

        else if (waterLevel < 0.25f)
        {

            source.clip = bubbleSounds_1_sek[Random.Range(0, bubbleSounds_1_sek.Length)];
        }



        source.volume = Random.Range(0.3f - volumeChangeMultiplier, 0.4f);
        source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        source.PlayOneShot(source.clip);


    }
}
