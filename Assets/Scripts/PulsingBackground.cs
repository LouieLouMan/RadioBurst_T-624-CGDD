using System.Linq;
using UnityEngine;
using UnityEngine.iOS;

public class PulsingBackground : MonoBehaviour
{
    public float step = 0.05f;
    public int gradientResolution = 2; 
    public Gradient gradient;
    public GameObject indicator;
    private readonly Color black = Color.black;
    private Color[] colorCycle;
    private float duration = 0f;
    private float pulseDuration = 0f;
    private int index = 0;
    private bool flip = false;
    private bool flop = false;
    private bool indicatorPulse = false;
    // Start is called before the first frame update
    void Start()
    {
        GenerateColorCycle();
    }

    // Update is called once per frame
    void Update()
    {
        if (indicatorPulse)
        {
            var indicators = indicator.GetComponentsInChildren<SpriteRenderer>();
            indicators.ToList().ForEach(item => item.color = Color.Lerp(Color.white, new(0f,0f,0f,0f), 1 * pulseDuration));
            pulseDuration += Time.deltaTime / step;
            if (pulseDuration >= 1f) 
            {
                pulseDuration = 0;
                indicatorPulse = false;
            }
        }
        if (flip)
        {
            if (flop) {
                index = (index + 1) % colorCycle.Length;
                flop = false;
                duration = 0;
            }
            Camera.main.backgroundColor = Color.Lerp(black, colorCycle[index], 1 * duration);
            duration += Time.deltaTime / step * 2;
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
            duration += Time.deltaTime / (step * 5);
        }
    }

    public void PulseBackground()
    {
        flip = true;
    }

    public void PulseBeatIndicator()
    {
        indicatorPulse = true;
    }

    private void GenerateColorCycle()
{
    // GENERATED BY CHATGPT
    // Ensure the resolution is at least 2 to avoid errors
    // Initialize the colorCycle array
    colorCycle = new Color[gradientResolution];

    // Sample the gradient at evenly spaced intervals
    for (int i = 0; i < gradientResolution; i++)
    {
        float t = i / (float)(gradientResolution - 1); // Normalized interval [0, 1]
        colorCycle[i] = gradient.Evaluate(t); // Sample the gradient
    }
}
}