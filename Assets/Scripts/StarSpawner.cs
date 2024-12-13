using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour
{
    public TextMeshProUGUI Text;
    RawImage sprite;
    Animator animator;
    GameObject myCamera;
    public ParticleSystem starParticles;
    
    int scoreTick;
    public int spawnScore;
    string lastText = "";
    bool spawned = false;
    bool played = false;
    public bool multiplierCondition = false;
    public bool missBeatCondition = false;
    public bool getHitCondition = false;
    bool conditionMet = false;
    // Start is called before the first frame update

    void Awake()
    {
        starParticles.Stop();
    }
    void Start()
    {
        sprite = GetComponent<RawImage>();
        animator = GetComponent<Animator>();
        myCamera = GameObject.Find("Main Camera");

        if (getHitCondition || missBeatCondition)
        {
            conditionMet = true;
        }
        
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Text.text != lastText)
        {
            scoreTick = int.Parse(Text.text, System.Globalization.NumberStyles.AllowThousands);
            lastText = Text.text;
        }
        
        //GET 10X
        if (multiplierCondition && !conditionMet)
        {
            if (GameControllerScript.instance.multiplier == 100)
            {
                conditionMet = true;
            }
        }

        //GET HIT > 15
        if (getHitCondition && conditionMet)
        {
            if (GameControllerScript.instance.hitCount > 15)
            {
                conditionMet = false;
            }
        }

         //MISS > 95% BEATS
        if (missBeatCondition && conditionMet)
        {
            if ((396f - GameControllerScript.instance.beatMiss)/396f < 0.85f)
            {
                conditionMet = false;
            }
        }


        if (conditionMet)
        {
            if (!spawned)
            {
            sprite.enabled = true;
            animator.SetTrigger("DropStar");
            played = true;
            spawned = true;
            }

            if (played && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                played = false;
                myCamera.GetComponent<Shake>().PlayerHitShake();
                StartCoroutine(PlayParticles());
            }
        }


    }

    private IEnumerator PlayParticles()
    {
        starParticles.Play();
        yield return new WaitForSeconds(1);
        starParticles.Stop();
    }
}
