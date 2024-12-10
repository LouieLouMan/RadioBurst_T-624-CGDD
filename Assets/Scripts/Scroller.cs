using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scroller : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollSpeed;
    MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer> ();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
