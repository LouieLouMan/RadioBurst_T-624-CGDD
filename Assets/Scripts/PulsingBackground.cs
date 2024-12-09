using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class PulsingBackground : MonoBehaviour
{
    public float step = 0.05f;
    private readonly Color black = Color.black;
    private readonly Color[] colorCycle = {
        new(0.3f, 0f, 0f, 0.1f),  // Dark Red
        new(0.3f, 0.1f, 0.05f, 0.1f),
        new(0.3f, 0.2f, 0.1f, 0.1f),
        new(0.3f, 0.3f, 0.15f, 0.1f),
        new(0.25f, 0.4f, 0.2f, 0.1f),
        new(0.2f, 0.5f, 0.25f, 0.1f),
        new(0.15f, 0.6f, 0.3f, 0.1f), // Greenish tone
        new(0.1f, 0.5f, 0.35f, 0.1f),
        new(0.05f, 0.4f, 0.4f, 0.1f),
        new(0f, 0.3f, 0.45f, 0.1f),
        new(0f, 0.25f, 0.5f, 0.1f),
        new(0f, 0.2f, 0.55f, 0.1f),
        new(0f, 0.15f, 0.6f, 0.1f),
        new(0.05f, 0.1f, 0.65f, 0.1f),
        new(0.1f, 0.05f, 0.7f, 0.1f),
        new(0.15f, 0f, 0.75f, 0.1f),
        new(0.2f, 0f, 0.8f, 0.1f),
        new(0.25f, 0f, 0.85f, 0.1f),
        new(0.3f, 0f, 0.9f, 0.1f),  // Dark Blue
    };


    private float duration = 0f;
    private int index = 0;
    private bool flip = false;
    private bool flop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flip)
        {
            Camera.main.backgroundColor = Color.Lerp(black, colorCycle[index], 1 * duration);
            duration += Time.deltaTime / step;
            if (duration >= 1f) 
            {
                flip = false;
                flop = true;
                duration = 0;
            }
        }
        if (flop)
        {
            Camera.main.backgroundColor = Color.Lerp(colorCycle[index], black, 1 * duration);
            duration += Time.deltaTime / step;;
            if (duration >= 1f) 
            {
                flop = false;
                duration = 0;
                index = (index + 1) % colorCycle.Length;
            }
        }
    }

    public void Pulse()
    {
        flip = true;
    }
}
