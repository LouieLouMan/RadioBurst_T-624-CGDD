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
    public bool doubleSpeed = false;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        lastHitKey = KeyCode.W;
    }

    // Update is called once per frame
    void Update()
    {
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
        GameControllerScript.instance.score -= math.min(GameControllerScript.instance.score, collisionScoreLoss);
        Debug.Log("Player collision");
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

