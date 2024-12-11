using UnityEngine;

public class Laser : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite phase1;
    public Sprite phase2;
    public Sprite phaseDamage;
    public SpriteRenderer laserglow;
    public Camera mainCamera;
    int lastBeat;

    // Start is called before the first frame update
    void Start()
    {
        lastBeat = AudioManager.instance.currentBeat;
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        spriteRenderer.sprite = phase1;
        laserglow.gameObject.SetActive(false);

    }

    void Update()
    {
       if (AudioManager.instance.currentBeat == (lastBeat+4)){
            spriteRenderer.sprite = phase2;
       }
       if (AudioManager.instance.currentBeat == (lastBeat+8)){
            spriteRenderer.sprite = phaseDamage;
            
            AudioManager.instance.PlayLaserSound();
            mainCamera.GetComponent<Shake>().LaserShake();
            GetComponent<BoxCollider2D>().enabled = true;
            laserglow.gameObject.SetActive(true);
       }
    }
    
}
