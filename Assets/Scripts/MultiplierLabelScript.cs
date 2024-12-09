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
        var pmain = particles.main;
        var pem = particles.emission;
        var pcol = particles.colorOverLifetime;
        multiplier = GameControllerScript.instance.multiplier;
        multiplier = multiplier * 0.1f;
        if (multiplier != last_mult) {
            _animator.SetTrigger("Pop");
            last_mult = multiplier;
        }
        if (multiplier >= 3 && multiplier < 10)
        {
            pmain.maxParticles = (int)(multiplier * multiplier * multiplier);
            pem.rateOverTime = multiplier * multiplier * multiplier / 10;

            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { new(new Color(255f/255f, 198f/255f, 117f/255f), 0.0f), new(new Color(255f/255f, 0f/255f, 0f/255f), 0.65f) }, new GradientAlphaKey[] { new(1.0f, 1.0f), new(1.0f, 1.0f) } );
            pcol.color = grad;

        }
        else if (multiplier >= 10)
        {
            Gradient grad = new Gradient();
            grad.SetKeys( new GradientColorKey[] { new(new Color(160f/255f, 255f/255f, 229f/255f), 0.0f), new(Color.blue, 0.65f) }, new GradientAlphaKey[] { new(1.0f, 1.0f), new(1.0f, 1.0f) } );
            pcol.color = grad;
        }
        else 
        {
            pmain.maxParticles = 0;
            pem.rateOverTime = 5;
        }
        scoreLabel.text = multiplier.ToString("F1") + "X";
    }
}
