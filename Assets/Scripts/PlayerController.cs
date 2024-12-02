using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public UnityEngine.Vector2 levelBounds;
    public Transform movePoint;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        float pointDirection_x = 0f;
        float pointDirection_y = 0f;
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            pointDirection_x = Input.GetAxisRaw("Horizontal");
            pointDirection_y = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(pointDirection_x) == 1f)
            {
                if (Mathf.Abs(movePoint.position.x) == levelBounds.x){
                    if (pointDirection_x > 0 && movePoint.position.x == levelBounds.x){
                        pointDirection_x = -pointDirection_x;
                    }
                    if (pointDirection_x < 0 && movePoint.position.x == -levelBounds.x){
                        pointDirection_x = -pointDirection_x;
                    }
                }
                movePoint.position += new Vector3(pointDirection_x,0,0);
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -90f * pointDirection_x);
            }
            if (Mathf.Abs(pointDirection_y) == 1f)
            {
                if (Mathf.Abs(movePoint.position.y) == levelBounds.y){
                    if (pointDirection_y > 0 && movePoint.position.y == levelBounds.y){
                        pointDirection_y = -pointDirection_y;
                    }
                    if (pointDirection_y < 0 && movePoint.position.y == -levelBounds.y){
                        pointDirection_y = -pointDirection_y;
                    }
                }
                movePoint.position += new Vector3(0,pointDirection_y,0);
                if (pointDirection_y > 0){
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else 
                {
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
                }
            }
        }
    }
}
