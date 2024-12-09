using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    public UnityEngine.Vector2 direction;

    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
       body.velocity = direction.normalized * speed;
       ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        StartCoroutine(JuicyExplode());
    }

    IEnumerator JuicyExplode()
    {
            body.velocity = direction.normalized * 0;
            ps.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);

    }
}
