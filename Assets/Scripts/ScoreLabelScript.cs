using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLabelScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreLabel;

    int currentScore;
    
    void Update()
    {
        currentScore = GameControllerScript.instance.score;
        scoreLabel.text = currentScore.ToString();
    }

}
