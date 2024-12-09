using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierLabelScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreLabel;

    float multiplier;
    float last_mult = -1f;

    public Animator _animator;
    
    void Update()
    {
        multiplier = GameControllerScript.instance.multiplier;
        multiplier = multiplier * 0.1f;
        if (multiplier != last_mult) {
            _animator.SetTrigger("Pop");
            last_mult = multiplier;
        }
        scoreLabel.text = multiplier.ToString("F1") + "X";
    }
}
