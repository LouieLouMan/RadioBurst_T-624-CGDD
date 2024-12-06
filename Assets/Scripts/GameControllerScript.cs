using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript instance;
    public int score = 0;
    void Awake()
    {
        instance = this;
        //Application.targetFrameRate = 120;
    }
}
