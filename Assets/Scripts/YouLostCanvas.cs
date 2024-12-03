using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
public class MainCanvasScript : MonoBehaviour
{
    GameObject child;

    int keyDownDuration = 300;
    int keyDownCount = 300;

    void Start()
    {
        child = this.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log(keyDownCount);
            // Toggle pause state on Escape key press
            if (keyDownCount >= keyDownDuration)
            {
                PauseGame();

            }
            else
            {
                keyDownCount++;
            }
        }
        else
        {
            keyDownCount = 0;
        }
    }

    void PauseGame()
    {
        // Set Time.timeScale to 0 to pause gameplay
        Time.timeScale = 0;
        // Make PauseMenu panel visible (activate its gameObject)
        child.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        // Set Time.timeScale back to 1 to resume gameplay
        Time.timeScale = 1;
        // Hide PauseMenu panel (deactivate its gameObject)
        child.gameObject.SetActive(false);
    }

    public void MainMenu ()
    {
        SceneManager.LoadScene(0);
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton ()
    {
        Application.Quit();
    }

}