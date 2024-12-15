using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript instance;
    public int score = 0;
    public int multiplier = 10;
    public int hitCount = 0;
    public int beatMiss = 0;
    public int maxMultiplier = 0;
    public bool gotTenX = false;
    void Awake()
    {
        instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 128; // <- waow
    }

    void Update()
    {
        if (multiplier > maxMultiplier)
        {
            maxMultiplier = multiplier;
        }

        if (multiplier == 100)
        {
            gotTenX = true;
        }
    }
}
