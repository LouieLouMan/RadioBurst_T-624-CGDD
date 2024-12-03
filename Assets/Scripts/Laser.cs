using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("This ran");
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("You suck");
        }
    }
}
