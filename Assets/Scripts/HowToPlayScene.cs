using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(0);
        }
    }
}
