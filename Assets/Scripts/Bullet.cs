using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    public UnityEngine.Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
       body.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
