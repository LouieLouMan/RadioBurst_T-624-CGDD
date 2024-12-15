using UnityEngine;

public class MultiplierLabelScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreLabel;

    float multiplier;
    float last_mult = 1f;

    public ParticleSystem redFire;
    public ParticleSystem blueFire;

    public Animator _animator;
    public Transform body;

    void Awake()
    {
        redFire.Stop();
        blueFire.Stop();
    }

    void Start() 
    {
        var pmain = redFire.main;
        pmain.maxParticles = 0;
        var pem = redFire.emission;
        pem.rateOverTime = 5;
    }
    
    void Update()
    {
        var pmain = redFire.main;
        var pem = redFire.emission;
        var pcol = redFire.colorOverLifetime;
        multiplier = GameControllerScript.instance.multiplier;
        multiplier = multiplier * 0.1f;
        if (multiplier != last_mult) {
            if (multiplier == 1)
            {
                _animator.StopPlayback();
                _animator.SetTrigger("multireset");
            }
            else
            {
            _animator.SetTrigger("Pop");
            }
            last_mult = multiplier;
        }
        if (multiplier >= 3 && multiplier < 10)
        {   if (!redFire.isPlaying)
            {
                redFire.Play();
            }
            if (blueFire.isPlaying)
            {
                blueFire.Stop();
            }
            pmain.maxParticles = (int)(multiplier * multiplier * multiplier);
            pem.rateOverTime = multiplier * multiplier * multiplier / 10;
        }
        else if (multiplier >= 10)
        {
            if (redFire.isPlaying)
            {
                redFire.Stop();
            }
            if (!blueFire.isPlaying)
            {
                blueFire.Play();
            }
        }
        else 
        {
            if (redFire.isPlaying)
            {
                redFire.Stop();
            }
            if (blueFire.isPlaying)
            {
                blueFire.Stop();
            }

            pmain.maxParticles = 0;
            pem.rateOverTime = 5;
        }
        scoreLabel.text = multiplier.ToString("F1") + "X";
    }
}
