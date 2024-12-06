using UnityEngine;

public class Laser : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite phase1;
    public Sprite phase2;
    public Sprite phaseDamage;
    int lastBeat;

    // Start is called before the first frame update
    void Start()
    {
        lastBeat = AudioManager.instance.currentBeat;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = phase1;
    }

    void Update()
    {
       if (AudioManager.instance.currentBeat == (lastBeat+4)){
            spriteRenderer.sprite = phase2;
       }
       if (AudioManager.instance.currentBeat == (lastBeat+8)){
            spriteRenderer.sprite = phaseDamage;
            GetComponent<BoxCollider2D>().enabled = true;
       }
    }
}
