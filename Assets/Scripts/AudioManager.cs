using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource song;
    public float bpm;
    private float spb;
    float timer;
    float lastTimer;
    // Start is called before the first frame update
    void Start()
    {
        song = GetComponent<AudioSource>();
        spb = 60f/bpm;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            timer = 0;
            lastTimer = 0;
            song.Play();
        }

        timer += Time.deltaTime;

        if (timer > spb && lastTimer <= spb){
            timer = 0;
            lastTimer = 0;
            Debug.Log("BEAT");
        }

        lastTimer = timer;
    }
}
