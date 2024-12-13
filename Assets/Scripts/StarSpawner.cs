using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour
{
    RawImage sprite;
    Animator animator;
    GameObject myCamera;
    public AudioSource starSoundSource;
    public AudioClip starLandSFX;
    public ParticleSystem starParticles;
    bool spawned = false;
    bool played = false;
    public bool multiplierCondition = false;
    public bool missBeatCondition = false;
    public bool getHitCondition = false;

    public TextMeshProUGUI playerStatText;

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

        if (missBeatCondition)
        {
            playerStatText.text = "GOT: " + (((396f - GameControllerScript.instance.beatMiss)/396f)*100f).ToString("F1") + "%";
        }
        
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //GET 10X
        if (multiplierCondition && !conditionMet)
        {
            playerStatText.text = "GOT: " + (GameControllerScript.instance.maxMultiplier * 0.1f).ToString("F1") + "X";
            if (GameControllerScript.instance.gotTenX)
            {
                conditionMet = true;
            }
        }

        //GET HIT > 15
        if (getHitCondition && conditionMet)
        {
            playerStatText.text = "GOT HIT " + GameControllerScript.instance.hitCount.ToString() + " TIMES";
            if (GameControllerScript.instance.hitCount > 10)
            {
                conditionMet = false;
            }
        }

         //MISS > 95% BEATS
        if (missBeatCondition && conditionMet)
        {
            if ((396f - GameControllerScript.instance.beatMiss)/396f < 0.90f)
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
                starSoundSource.PlayOneShot(starLandSFX);
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
