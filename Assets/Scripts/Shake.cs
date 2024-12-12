using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve Curve;
    public AnimationCurve LaserCurve;
    public float duration = 1.5f;
    public float laserShakeDuration = 0.5f;
    public float playerShakeDuration = 0.1f;
    

    private Coroutine currentShake;

    public void PlayerHitShake()
    {  
            if(currentShake != null){
                StopCoroutine(currentShake);
            }
            currentShake = StartCoroutine(PlayerHitShakeEnumerator()); 
    }

    IEnumerator PlayerHitShakeEnumerator(){
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            float strength = Curve.Evaluate(elapsedTime/duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
        currentShake = null; 

    }
    public void LaserShake()
    {
        if(currentShake == null){
            currentShake = StartCoroutine(LaserShakeEnumerator());
        }
    }

    IEnumerator LaserShakeEnumerator()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = LaserCurve.Evaluate(elapsedTime / laserShakeDuration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
        currentShake = null; 
    }

    public void OffBeatShake()
    {
        if(currentShake != null){
            StopCoroutine(currentShake);
        }
        currentShake = StartCoroutine(OffBeatShakeEnumerator());
    }

    IEnumerator OffBeatShakeEnumerator()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = LaserCurve.Evaluate(elapsedTime / playerShakeDuration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
        currentShake = null; 
    }
}
