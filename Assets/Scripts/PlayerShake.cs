using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShake : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer playerSprite;
    public AnimationCurve LaserCurve;
    public Coroutine currentShake;
    public float duration = 1.5f;
    public float laserShakeDuration = 0.5f;
    public float playerShakeDuration = 0.1f;
    private float offBeatTimer = 0;

    void Update()
    {
        offBeatTimer += Time.deltaTime;
    }
    public void OffBeatShake()
    {
        if(currentShake != null){
            StopCoroutine(currentShake);
        }
        if (offBeatTimer > 0.25f)
        {
            currentShake = StartCoroutine(OffBeatShakeEnumerator());
            offBeatTimer = 0;
        }
    }
    IEnumerator OffBeatShakeEnumerator()
    {
        Vector3 startPosition = playerSprite.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < playerShakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = LaserCurve.Evaluate(elapsedTime / playerShakeDuration);
            playerSprite.transform.position= startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        playerSprite.transform.position = startPosition;
        currentShake = null; 
    }
}
