using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public int movementScoreGain = 10;
    public int collisionScoreLoss = 100;
    public UnityEngine.Vector2 levelBounds;
    public Transform movePoint;
    private KeyCode lastHitKey;
    public Collider2D playerCollider; 
    public ParticleSystem playerParticle;
    public SpriteRenderer playerSprite;
    public AudioSource playerSourceSFX;
    public AudioClip PlayerClipSFX;
    public Camera mainCamera;
    public float hitOnBeat;
    public bool doubleSpeed = false;
    public int INVINCIBLE_FRAMES;
    public bool isInvincible = false;
    public AudioMixer gameAudioMixer;
    public float muffledFrequency = 500f;
    public float normalFrequency = 22000f;
    public float muffledDuration = 1.5f;
    public float warpedPitch = 0.8f;
    public float normalPitch = 1.0f;
    Animator scoreAnimator;



    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        lastHitKey = KeyCode.W;
        ResetAudioEffects();
        scoreAnimator = GameObject.Find("UiPlayerScore").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastHitKey = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastHitKey = KeyCode.D;
        }
 
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            lastHitKey = KeyCode.W;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastHitKey = KeyCode.S;
        }

        transform.position = Vector3.Lerp(transform.position, movePoint.position, Ease(moveSpeed * Time.deltaTime));
        PlayerInvincibility();

    }

    void OnTriggerStay2D(Collider2D other)
    {
        mainCamera.GetComponent<Shake>().PlayerHitShake();
        hitOnBeat = AudioManager.instance.currentBeat; 
        playerParticle.Play();
        playerSourceSFX.PlayOneShot(PlayerClipSFX);
        
        isInvincible = true;
        playerCollider.enabled = false;
        playerSprite.enabled = false;
        
        
        GameControllerScript.instance.score -= math.min(GameControllerScript.instance.score, collisionScoreLoss);
        GameControllerScript.instance.multiplier = 0;

        MuffleAudio();
        Invoke(nameof(ResetAudioEffects), muffledDuration);
    }

    void PlayerInvincibility(){

        if(isInvincible){
            if(AudioManager.instance.currentBeat < hitOnBeat+INVINCIBLE_FRAMES){
                if(AudioManager.instance.currentBeat % 2 == 0){
                    playerSprite.enabled = true;
                }else {
                    playerSprite.enabled = false;
                }
            }

            if (AudioManager.instance.currentBeat >= hitOnBeat+INVINCIBLE_FRAMES){
                playerParticle.Clear();
                playerCollider.enabled = true;
                playerSprite.enabled = true;
                isInvincible = false;
            }
        }
    }

    public void MovePlayer()
    {
        GameControllerScript.instance.multiplier += 1;
        GameControllerScript.instance.score += (int)(10 * (GameControllerScript.instance.multiplier * 0.1f));
        scoreAnimator.SetTrigger("scorePop");
        Vector2 pointDirection = createVector();
        if (Mathf.Abs(pointDirection.x) == 1f)
            {
                if (Mathf.Abs(movePoint.position.x) == levelBounds.x){
                    if (pointDirection.x > 0 && movePoint.position.x == levelBounds.x){
                        pointDirection.x = -pointDirection.x;
                        lastHitKey = KeyCode.A;
                    }
                    if (pointDirection.x < 0 && movePoint.position.x == -levelBounds.x){
                        pointDirection.x = -pointDirection.x;
                        lastHitKey = KeyCode.D;
                    }
                }
                movePoint.position += new Vector3(pointDirection.x,0,0);
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -90f * pointDirection.x);
            }
            if (Mathf.Abs(pointDirection.y) == 1f)
            {
                if (Mathf.Abs(movePoint.position.y) == levelBounds.y){
                    if (pointDirection.y > 0 && movePoint.position.y == levelBounds.y){
                        pointDirection.y = -pointDirection.y;
                        lastHitKey = KeyCode.S;
                    }
                    if (pointDirection.y < 0 && movePoint.position.y == -levelBounds.y){
                        pointDirection.y = -pointDirection.y;
                        lastHitKey = KeyCode.W;
                    }
                }
                movePoint.position += new Vector3(0,pointDirection.y,0);
                if (pointDirection.y > 0){
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else 
                {
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
                }
            }
    }

    Vector2 createVector(){
        if (lastHitKey == KeyCode.A)
            return new Vector2(-1,0);
        if (lastHitKey == KeyCode.D)
            return new Vector2(1,0);
        if (lastHitKey == KeyCode.W)
            return new Vector2(0,1);
        if (lastHitKey == KeyCode.S)
            return new Vector2(0,-1);
        
        return new Vector2(0,0);
    }
    void MuffleAudio()
    {
        // Lower the cutoff frequency to muffle
        gameAudioMixer.SetFloat("LowPassFreq", muffledFrequency);
        
        // Lower the pitch to warp
        gameAudioMixer.SetFloat("MasterPitch", warpedPitch);
    }

    void ResetAudioEffects()
    {
        // Restore the normal cutoff frequency
        gameAudioMixer.SetFloat("LowPassFreq", normalFrequency);
        
        // Restore the normal pitch
        gameAudioMixer.SetFloat("MasterPitch", normalPitch);
    }

    float Ease(float x)
    {
        return x * x;
    }
}

