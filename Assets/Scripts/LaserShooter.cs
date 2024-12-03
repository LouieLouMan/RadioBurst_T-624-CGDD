using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public enum EnemyType
    {
        laser,
    }

    [Serializable]
    public class Enemy
    {
        public int beat;
        public int x_pos;
        public int y_pos;
        public int degrees;
        public EnemyType enemyType;
    }
    public Enemy[] enemies;
    public GameObject laser;
    public int loopTime;
    private int lastBeat = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioManager.instance.currentBeat > lastBeat)
        {
            lastBeat = AudioManager.instance.currentBeat;
            createEnemy();
        }
    }

    void createEnemy(){
        Enemy[] enemiesOnBeat = enemies.Where(e => e.beat == lastBeat % loopTime).ToArray();
        foreach (Enemy e in enemiesOnBeat)
        {
            Debug.Log(e.beat);
            if (e.enemyType == EnemyType.laser)
            {
                bool isHorizontal = e.degrees == 90;
                int pos = isHorizontal ? e.y_pos : e.x_pos;
                spawnLaserAt(pos, isHorizontal);
            }
        }

    }


    void spawnLaserAt(int position, bool horizontal)
    {
        Vector3 laserPosition;
        if (horizontal)
        {
            laserPosition = new Vector3(0, position, 0);
        }
        else
        {
            laserPosition = new Vector3(position, 0 ,0);
        }
        Quaternion rotation = horizontal ? Quaternion.Euler(0, 0, 90) : Quaternion.identity;
        GameObject laserShot = Instantiate(laser, laserPosition, rotation);
        Destroy(laserShot, 1);
    }
}
