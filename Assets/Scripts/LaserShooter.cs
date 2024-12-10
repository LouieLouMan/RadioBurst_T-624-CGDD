using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using UnityEditor;

public class LaserShooter : MonoBehaviour
{
    public enum EnemyType
    {
        laser = 0,
        bullet = 1,
    }

    [Serializable]
    public class Enemy
    {
        public int beat;
        public int x_pos;
        public int y_pos;
        public int degrees;
        public float speed;
        public EnemyType enemyType;

        public Enemy(int beat, int x_pos, int y_pos, int degrees, EnemyType enemyType, float speed){
            this.beat = beat;
            this.x_pos = x_pos;
            this.y_pos = y_pos;
            this.degrees = degrees;
            this.enemyType = enemyType;
            this.speed = speed;
        }
    }
    public List<Enemy> enemies;
    public GameObject laser;
    public GameObject bullet;
    public int loopTime;
    private int lastBeat = -1;
    // Start is called before the first frame update
    void Start()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Levels/level_bok");
        var reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            var line = reader.ReadLine();
            var values = line.Split(';');
            enemies.Add(
                new Enemy(
                    int.Parse(values[0]), 
                    int.Parse(values[1]), 
                    int.Parse(values[2]),
                    int.Parse(values[3]),
                    (EnemyType)int.Parse(values[4]),
                    float.Parse(values[5])
                )
            );
        }
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
        var enemiesOnBeat = enemies.Where(e => e.beat == lastBeat % loopTime).ToArray();
        foreach (Enemy e in enemiesOnBeat)
        {
            if (e.enemyType == EnemyType.laser)
            {
                bool isHorizontal = e.degrees == 90;
                int pos = isHorizontal ? e.y_pos : e.x_pos;
                spawnLaserAt(pos, isHorizontal);
            }
            if (e.enemyType == EnemyType.bullet)
            {
                spawnBulletAt(e.x_pos, e.y_pos, e.degrees, e.speed);
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
        Destroy(laserShot, AudioManager.instance.spb * 4);
    }

    void spawnBulletAt(int x_pos, int y_pos, int degrees, float speed)
    {
        Vector3 bulletPosition = new Vector3(x_pos, y_pos, 0);
        GameObject bulletShot = Instantiate(bullet, bulletPosition, Quaternion.identity);
        var x = bulletShot.GetComponent<Bullet>();
        x.speed = speed;
        bulletShot.GetComponent<Bullet>().direction = Quaternion.Euler(0f,0f,degrees) * x.direction * x.speed;
        Destroy(bulletShot, 15);
    }
}
