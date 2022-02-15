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

    private WaterLevel wl;

    private
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        wl = GameObject.Find("WaterTank").GetComponent<WaterLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && wl)
        {
            source.clip = bubbleSounds_4_sek[Random.Range(0, bubbleSounds_4_sek.Length)];
            source.volume = Random.Range(0.6f - volumeChangeMultiplier, 0.6f);
            source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
            source.PlayOneShot(source.clip);
        }
    }
}
