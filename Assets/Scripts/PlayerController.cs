using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public int movementScoreGain = 10;
    public int collisionScoreLoss = 100;
    public UnityEngine.Vector2 levelBounds;
    public Transform movePoint;
    private KeyCode lastHitKey;
    public Collider2D playerCollider; 
    public SpriteRenderer playerSprite;
    public AudioSource dmg;
    public float hitOnBeat;
    public bool doubleSpeed = false;
    public int INVINCIBLE_FRAMES;
    public bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        lastHitKey = KeyCode.W;
        dmg = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible == true){
            PlayerContinuousCollisions();
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastHitKey = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastHitKey = KeyCode.D;
        }
 
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastHitKey = KeyCode.W;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastHitKey = KeyCode.S;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            doubleSpeed = !doubleSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        hitOnBeat = AudioManager.instance.currentBeat; 
        dmg.Play();
        
        playerCollider.enabled = false;
        playerSprite.enabled = false;
        isInvincible = true;
        GameControllerScript.instance.score -= math.min(GameControllerScript.instance.score, collisionScoreLoss);
    }

    void PlayerContinuousCollisions(){

        if(AudioManager.instance.currentBeat < hitOnBeat+INVINCIBLE_FRAMES){
            if(AudioManager.instance.currentBeat % 2 == 0){
                playerSprite.enabled = true;
            }else {
                playerSprite.enabled = false;
            }
        }

        if (AudioManager.instance.currentBeat >= hitOnBeat+INVINCIBLE_FRAMES){
            playerCollider.enabled = true;
            playerSprite.enabled = true;
            isInvincible = false;
        }
    }

    public void MovePlayer()
    {
        GameControllerScript.instance.score += 10;
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
}

