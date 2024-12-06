using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuCanvas : MonoBehaviour
{   
    GameObject lastSelection;

    void Update()
    {   

        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelection);
        }
        else
        {
            lastSelection = EventSystem.current.currentSelectedGameObject;
        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton ()
    {   
        Application.Quit();
    }

}
