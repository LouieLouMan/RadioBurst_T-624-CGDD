using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript instance;
    public int score = 0;
    public int multiplier = 10;
    void Awake()
    {
        instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 128; // <- waow
    }
}
