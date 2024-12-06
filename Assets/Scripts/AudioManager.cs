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

    public GameObject pressSpaceToStartTxt;

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
            }

            if (beatTimer > spb/4 && lastBeatTimer <= spb/4)
            {
                beatTimer = 0;
                lastBeatTimer = 0;
                currentBeat++;
                Debug.Log(currentBeat);
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
