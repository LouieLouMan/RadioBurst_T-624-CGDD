using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource song;
    public float bpm;
    public float spb;
    float timer;
    float lastTimer;
    float beatTimer;
    float lastBeatTimer;
    public GameObject player;
    public int currentBeat;
    public bool isPlaying = false;
    public static AudioManager instance;

    // Start is called before the first frame update
    
    void Awake()
    {
        instance = this;
    }
    
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
            currentBeat = 1;
            beatTimer = 0;
            lastBeatTimer = 0;
            song.Play();
            isPlaying = true;
        }

        timer += Time.deltaTime;
        beatTimer += Time.deltaTime;

        if (isPlaying){
            if (beatTimer > spb/4 && lastBeatTimer <= spb/4)
            {
                beatTimer = 0;
                lastBeatTimer = 0;
                currentBeat++;
            }

            if (timer > spb/2 && lastTimer <= spb/2 && player.GetComponent<PlayerController>().doubleSpeed){
                player.GetComponent<PlayerController>().MovePlayer();
            }

            if (timer > spb && lastTimer <= spb){
                timer = 0;
                lastTimer = 0;
                player.GetComponent<PlayerController>().MovePlayer();
            }

            lastTimer = timer;
            lastBeatTimer = beatTimer;
        }
    }
}
