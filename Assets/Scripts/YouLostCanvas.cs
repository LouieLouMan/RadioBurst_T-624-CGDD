using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class MainCanvasScript : MonoBehaviour
{
    GameObject youLostMenu;

    public int baseOpacity = 0;

    void Start()
    {
        youLostMenu = this.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        // Set Time.timeScale to 0 to pause gameplay
        Time.timeScale = 0;
        // Make PauseMenu panel visible (activate its gameObject)
        youLostMenu.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        // Set Time.timeScale back to 1 to resume gameplay
        Time.timeScale = 1;
        // Hide PauseMenu panel (deactivate its gameObject)
        youLostMenu.gameObject.SetActive(false);
    }
    
    public void RetryGame()
    {
        ResumeGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void MainMenu ()
    {   
        ResumeGame();
        SceneManager.LoadScene(0);
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton ()
    {   
        ResumeGame();
        Application.Quit();
    }

}