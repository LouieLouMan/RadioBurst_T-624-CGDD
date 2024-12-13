using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using Unity.VisualScripting;
public class GameOverCanvas : MonoBehaviour
{
    public static GameOverCanvas instance;

    public GameObject player;
    GameObject youLostMenu;
    
    GameObject lastSelection;
    GameObject playingUI;
    public RawImage cursor;
    public AudioSource audioSource;
    public RawImage rawImage;
    public float animationSpeed = 0.15f;
    public float scale = 10f;
    public float scaleFactor = 3f;
    public float offset = 20f;
    public AudioClip hoverSoundSFX;
    public AudioClip selectSFX;
    private Button[] buttons;

    private int i = 0;
    private float t = 0;

    public int baseOpacity = 0;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        youLostMenu = transform.GetChild(0).gameObject;
        playingUI = GameObject.Find("GameUiCanvas");

        buttons = new Button[2];
        foreach (var btn in Resources.FindObjectsOfTypeAll(typeof(Button)))
        {
            buttons[0] = btn.name == "RetryButton" ? btn.GetComponent<Button>() : buttons[0];
            buttons[1] = btn.name == "MainMenuButton" ? btn.GetComponent<Button>() : buttons[1];
        }

        buttons[0].onClick.AddListener(() => SceneManager.LoadScene(1));
        buttons[1].onClick.AddListener(() => SceneManager.LoadScene(1));

        cursor.rectTransform.anchoredPosition = new(buttons[0].GetComponent<RectTransform>().anchoredPosition.x - 120f, -217f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) AudioManager.instance.currentBeat = 1500;
        if (AudioManager.instance.isPlaying) return;


        t += Time.deltaTime;
        if (t > 10 * Mathf.PI) t = 0;
        float bounceScale = BounceFunction(t);
        rawImage.transform.localScale = scale * bounceScale * Vector3.one;


        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelection);
        }
        else
        {
            lastSelection = EventSystem.current.currentSelectedGameObject;
        }

        UpdateCursorPosition();
        if (Input.GetKey(KeyCode.L))
        {
            MainMenu();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventSystem.current.SetSelectedGameObject(GetNextSelection());
            audioSource.PlayOneShot(hoverSoundSFX);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventSystem.current.SetSelectedGameObject(GetPreviousSelection());
            audioSource.PlayOneShot(hoverSoundSFX);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            if (EventSystem.current.currentSelectedGameObject == buttons[0].gameObject)
            {
                RetryGame();
            }
            else if (EventSystem.current.currentSelectedGameObject == buttons[1].gameObject)
            {
                MainMenu();
            } 
        }
    }

    private GameObject GetNextSelection()
    {
        i = (i + 1) % buttons.Length;
        return buttons[i].gameObject;
    }

    private GameObject GetPreviousSelection()
    {
        i = i - 1 < 0 ? buttons.Length - 1 : i - 1;    
        return buttons[i].gameObject;
    }

    public void PauseGame()
    {
        // Set Time.timeScale to 0 to pause gameplay
        //Time.timeScale = 0;
        // Make PauseMenu panel visible (activate its gameObject)
        player.SetActive(false);
        playingUI.SetActive(false);
        youLostMenu.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        // Set Time.timeScale back to 1 to resume gameplay
        Time.timeScale = 1;
        // Hide PauseMenu panel (deactivate its gameObject)
        player.SetActive(true);
        playingUI.SetActive(true);
        youLostMenu.gameObject.SetActive(false);
    }
    
    public void RetryGame()
    {
        ResumeGame();
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Trying");
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }
    public void MainMenu ()
    {   
        ResumeGame();
        SceneManager.LoadScene(0);
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton ()
    {   
        Application.Quit();
    }

    private void UpdateCursorPosition()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            RectTransform selectedButtonRect = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
            if (selectedButtonRect != null)
            {
                cursor.rectTransform.anchoredPosition = new Vector2(
                    selectedButtonRect.anchoredPosition.x - 120f,
                    -217f
                );
            }
        }
    }

    private float BounceFunction(float x)
    {
        return (float) (offset + (Math.Cos(x) / scaleFactor)); 
    }

}