using UnityEngine;

public class MetronomeController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager audioManager;

    public Animator animator;
    public float beatsPerc;

    float timer;
    float perc;
    public Vector3 topScreen = new(-7,2,0);
    public  Vector3 bottomScreen = new(-7,-2,0);

    public Vector3 middleScreen = new(-7,0,0);
    public Vector3 tmpVector;

    void Start()
    {
        transform.position = topScreen;
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioManager.instance.isPlaying){

            //float ease = beatsPerc * beatsPerc * beatsPerc;
            float perc = timer / AudioManager.instance.spb;
            transform.position = Vector3.Lerp(topScreen, bottomScreen, perc);
            
            //print($"perc: +{perc} timer: {timer} spb: {AudioManager.instance.spb}");
            timer += Time.deltaTime;

            if(timer >= AudioManager.instance.spb){
                timer = 0;
                tmpVector = topScreen;
                topScreen = bottomScreen;
                bottomScreen = tmpVector;
            }
            

        }
    }
}
