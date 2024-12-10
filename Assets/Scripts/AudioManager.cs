using System;
using TMPro;
using Unity.Mathematics;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class AudioManager : MonoBehaviour
{

    public AudioSource song;
    public float bpm;
    public float spb;
    public float inputTolerance = 0.15f;
    float timer;
    float lastTimer;
    float beatTimer;
    float lastBeatTimer;
    bool cameraOdd = false;
    public GameObject player;
    public int currentBeat;
    public bool isPlaying = false;
    public static AudioManager instance;

    public GameObject pressSpaceToStartTxt;

    private Color black = Color.black;
    private Color crimson = new(70f/255f, 0, 70f/255f, 0.75f);
    private bool movedOnBeat = false;
    private float graceCooldown;

    // Start is called before the first frame update
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        song = GetComponent<AudioSource>();
        spb = 60f/bpm;
        GetComponent<AudioSource>().clip.LoadAudioData();
        graceCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPlaying == false){
            timer = 0;
            lastTimer = 0;
            currentBeat = 1;
            beatTimer = 0;
            lastBeatTimer = 0;
            song.Play();
            isPlaying = true;
            pressSpaceToStartTxt.gameObject.SetActive(false);
        }

        timer += Time.deltaTime;
        beatTimer += Time.deltaTime;
        graceCooldown += Time.deltaTime;

        if (isPlaying){
            if (currentBeat >= 900)
            {
                GameOverCanvas.instance.PauseGame();
                isPlaying = false;
            }


            if (beatTimer > spb/4 && lastBeatTimer <= spb/4)
            {
                beatTimer = 0;
                lastBeatTimer = 0;
                currentBeat++;
            }

            if (timer > spb && lastTimer <= spb)
            {
                timer %= spb;
                Camera.main.backgroundColor = cameraOdd ? crimson : black;
                cameraOdd = !cameraOdd;

                if (!movedOnBeat && graceCooldown > spb * 1.5f)
                {
                    GameControllerScript.instance.multiplier = Mathf.Max(0, GameControllerScript.instance.multiplier - 2);
                }
            }

            if (graceCooldown > 0.25f)
            {
                movedOnBeat = false;
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                if (IsInputOnBeat() && !movedOnBeat)
                {
                    movedOnBeat = true;
                    graceCooldown = 0;
                    player.GetComponent<PlayerController>().MovePlayer();
                    Debug.Log("Input was on beat!");
                }
                else
                {
                    Debug.Log("Input was off-beat!");
                }
            }

            lastTimer = timer;
            lastBeatTimer = beatTimer;
        }
    }
    bool IsInputOnBeat()
    {
        float timeSinceLastBeat = Mathf.Abs(timer - spb);
        Debug.Log(timeSinceLastBeat + " " + spb + " <- wtf");
        return timeSinceLastBeat <= inputTolerance || spb - timeSinceLastBeat <= inputTolerance;
    }
}
