using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour
{
    public TextMeshProUGUI Text;
    RawImage sprite;
    int scoreTick;
    public int spawnScore;
    string lastText = "";
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<RawImage>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Text.text != lastText)
        {
            scoreTick = int.Parse(Text.text, System.Globalization.NumberStyles.AllowThousands);
            lastText = Text.text;
        }
        
        if (scoreTick >= spawnScore)
        {
            sprite.enabled = true;
        }


    }
}
