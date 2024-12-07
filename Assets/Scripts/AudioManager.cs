using Unity.Mathematics;
using UnityEngine;

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
    private Color crimson = new Color(0.25f, 0f, 0f, 0.2f);

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

            // if (timer > spb/2 && lastTimer <= spb/2 && player.GetComponent<PlayerController>().doubleSpeed){
            //     player.GetComponent<PlayerController>().MovePlayer();
            // }

            if (timer > spb && lastTimer <= spb)
            {
                timer %= spb;
                Camera.main.backgroundColor = cameraOdd ? crimson : black;
                cameraOdd = !cameraOdd;
            }
            
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                if (IsInputOnBeat())
                {
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
        Debug.Log(timeSinceLastBeat);
        return timeSinceLastBeat <= inputTolerance || spb - timeSinceLastBeat <= inputTolerance;
    }
}
