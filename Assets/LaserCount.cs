using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCount : MonoBehaviour
{
    public Sprite targetSprite;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Laser[] lasers = FindObjectsOfType<Laser>();
        if(lasers.Length != 0){
            print(lasers.Length);
            AudioManager.instance.PlayLaserSound();
        }
    }
}

