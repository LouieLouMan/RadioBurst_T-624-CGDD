using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainCanvasScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Play()
    {
        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
}
