using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MultiplierLabelScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreLabel;

    float multiplier;
    float last_mult = 0f;

    public ParticleSystem particles;

    public Animator _animator;
    public Transform body;

    void Start() 
    {
        var pmain = particles.main;
        pmain.maxParticles = 0;
        var pem = particles.emission;
        pem.rateOverTime = 5;
    }
    
    void Update()
    {
        multiplier = GameControllerScript.instance.multiplier;
        multiplier = multiplier * 0.1f;
        if (multiplier != last_mult) {
            _animator.SetTrigger("Pop");
            last_mult = multiplier;
        }
        if (multiplier >= 3)
        {
            var pmain = particles.main;
            pmain.maxParticles = (int)(multiplier * multiplier * multiplier);
            var pem = particles.emission;
            pem.rateOverTime = multiplier * multiplier * multiplier / 10;
        }
        else 
        {
            var pmain = particles.main;
            pmain.maxParticles = 0;
            var pem = particles.emission;
            pem.rateOverTime = 5;
        }
        scoreLabel.text = multiplier.ToString("F1") + "X";
    }
}
