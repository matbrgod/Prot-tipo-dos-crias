using UnityEngine;
using UnityEngine.SceneManagement;

public class CenasManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f; // Resume the game
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Menu")
        {
            MusicManager.Instance.PlayMusic("Menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
