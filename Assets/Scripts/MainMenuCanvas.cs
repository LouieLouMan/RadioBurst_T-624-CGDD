using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;

public class MainMenuCanvas : MonoBehaviour
{   
    GameObject lastSelection;
    public RawImage cursor;
    public SpriteRenderer spriteRenderer;
    public float animationSpeed = 0.15f;
    public float scale = 10f;
    public float scaleFactor = 3f;
    private AudioSource mainMenuSong;
    private Button[] buttons;
    private int i = 0;
    private float t = 0;
    private AsyncOperation preloadGame;

    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => StartCoroutine(SelectPlay()));
        buttons[1].onClick.AddListener(() => StartCoroutine(SelectOther(2)));
        buttons[2].onClick.AddListener(() => StartCoroutine(SelectOther(3)));

        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);

        preloadGame = SceneManager.LoadSceneAsync(1);
        preloadGame.allowSceneActivation = false;

        if (mainMenuSong == null)
        {
            mainMenuSong = FindObjectOfType<AudioSource>();
            mainMenuSong.mute = false;
            DontDestroyOnLoad(mainMenuSong);
        }
    }

    void Update()
    {   
        t += Time.deltaTime;
        if (t > 10 * Mathf.PI) t = 0;
        
        float bounceScale = BounceFunction(t);
        spriteRenderer.transform.localScale = scale * bounceScale * Vector3.one;

        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelection);
        }
        else
        {
            lastSelection = EventSystem.current.currentSelectedGameObject;
        }

        UpdateCursorPosition();

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            EventSystem.current.SetSelectedGameObject(GetNextSelection());
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            EventSystem.current.SetSelectedGameObject(GetPreviousSelection());
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            if (EventSystem.current.currentSelectedGameObject == buttons[0].gameObject)
            {
                StartCoroutine(SelectPlay());
            }
            else if (EventSystem.current.currentSelectedGameObject == buttons[1].gameObject)
            {
                StartCoroutine(SelectOther(2));
            } 
            else if (EventSystem.current.currentSelectedGameObject == buttons[2].gameObject)
            {
                StartCoroutine(SelectOther(3));
            }
        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    // Called when we click the "Quit" button.

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

    private void UpdateCursorPosition()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            RectTransform selectedButtonRect = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
            if (selectedButtonRect != null)
            {
                cursor.rectTransform.anchoredPosition = new Vector2(
                    -150f,
                    selectedButtonRect.anchoredPosition.y
                );
            }
        }
    }

    private float BounceFunction(float x)
    {
        return (float) (20f + (Math.Cos(x) / scaleFactor)); 
    }

    private IEnumerator SelectPlay()
    {
        Debug.Log("hello from the coroutine");
        for (int i = 0; i < 12; i++)
        {
            cursor.enabled = !cursor.enabled;
            yield return new WaitForSeconds(0.05f);
        }
        cursor.enabled = true;

        RectTransform cursorTransform = cursor.rectTransform;
        RectTransform buttonTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
        Vector2 targetPosition = buttonTransform.anchoredPosition + new Vector2(50f, 0f);

        float elapsedTime = 0f;
        float duration = 0.3f;
        Vector2 startPosition = cursorTransform.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cursorTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        cursorTransform.anchoredPosition = targetPosition;
        Destroy(mainMenuSong);
        preloadGame.allowSceneActivation = true;
    }

    private IEnumerator SelectOther(int index)
    {
        Debug.Log("hello from the coroutine");
        for (int i = 0; i < 12; i++)
        {
            cursor.enabled = !cursor.enabled;
            yield return new WaitForSeconds(0.05f);
        }
        cursor.enabled = true;

        RectTransform cursorTransform = cursor.rectTransform;
        RectTransform buttonTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
        Vector2 targetPosition = buttonTransform.anchoredPosition + new Vector2(50f, 0f);

        float elapsedTime = 0f;
        float duration = 0.3f;
        Vector2 startPosition = cursorTransform.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cursorTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        cursorTransform.anchoredPosition = targetPosition;
        SceneManager.LoadScene(index);
    }
}
