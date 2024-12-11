using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MainMenuCanvas : MonoBehaviour
{   
    GameObject lastSelection;
    public RawImage cursor;
    public SpriteRenderer spriteRenderer;
    public float animationSpeed = 0.15f;
    public float scale = 10f;
    public float scaleFactor = 3f;
    private Button[] buttons;
    private int i = 0;
    private float t = 0;

    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }

    void Update()
    {   
        t += Time.deltaTime;
        if (t > 10 * Mathf.PI) t = 0;
        
        float bounceScale = BounceFunction(t);
        Debug.Log(bounceScale);
        spriteRenderer.transform.localScale = scale * bounceScale * Vector3.one;

        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelection);
        }
        else
        {
            lastSelection = EventSystem.current.currentSelectedGameObject;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            EventSystem.current.SetSelectedGameObject(GetNextSelection());
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            EventSystem.current.SetSelectedGameObject(GetPreviousSelection());
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (EventSystem.current == buttons[0].gameObject)
            {
                PlayGame();
            } else if (EventSystem.current == buttons[2].gameObject)
            {
                Application.Quit();
            }
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

    private GameObject GetNextSelection()
    {
        i = (i + 1) % buttons.Length;

        cursor.rectTransform.anchoredPosition = 
            i == 0 ? 
            new Vector2(-150f, -119f) :  
            new Vector2(cursor.rectTransform.anchoredPosition.x, cursor.rectTransform.anchoredPosition.y - 50f);

        return buttons[i].gameObject;
    }

    private GameObject GetPreviousSelection()
    {
        i = i - 1 < 0 ? buttons.Length - 1 : i - 1;
    
        cursor.rectTransform.anchoredPosition = 
            i == buttons.Length - 1 ? 
            new Vector2(-150f, -219f) :
            new Vector2(cursor.rectTransform.anchoredPosition.x, cursor.rectTransform.anchoredPosition.y + 50f);
        return buttons[i].gameObject;
    }

    private float BounceFunction(float x)
    {
        return (float) (20f + (Math.Cos(x) / scaleFactor)); 
    }
}
