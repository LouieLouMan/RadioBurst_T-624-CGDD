using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    public UnityEngine.Vector2 direction;
    public float elapsedTime = 0f;
    public float timeBeforeDestroy = 1.0f;
    float cameraHeight;
    float cameraWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        body.velocity = direction.normalized * speed;
        cameraHeight = FindObjectOfType<Camera>().orthographicSize * 2f;
        cameraWidth = cameraHeight * FindObjectOfType<Camera>().aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) > (cameraWidth/2)+2 || Mathf.Abs(transform.position.y) > (cameraHeight/2)+2)
        {
            Debug.Log("DELETE");
            Destroy(gameObject);
        }
        
    }
    //destroy the object after 
    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "ShadowLightning"){
            Destroy(gameObject);
        }
    }
}
