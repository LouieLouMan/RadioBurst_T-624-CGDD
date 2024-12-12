using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour
{
    public TextMeshProUGUI Text;
    RawImage sprite;
    Animator animator;
    GameObject myCamera;
    
    int scoreTick;
    public int spawnScore;
    string lastText = "";
    bool spawned = false;
    bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<RawImage>();
        animator = GetComponent<Animator>();
        myCamera = GameObject.Find("Main Camera");
        
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
            if (!spawned)
            {
            sprite.enabled = true;
            animator.SetTrigger("DropStar");
            played = true;
            spawned = true;
            }

            if (played && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                played = false;
                myCamera.GetComponent<Shake>().start = true;
            }
        }


    }
}
