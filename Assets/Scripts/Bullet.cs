using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    public UnityEngine.Vector2 direction;
    public float elapsedTime = 0f;
    public float timeBeforeDestroy = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
       body.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    //destroy the object after 
    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "ShadowLightning"){
            Destroy(gameObject);
        }
    }
}
