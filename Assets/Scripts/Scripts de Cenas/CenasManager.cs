using UnityEngine;
using UnityEngine.SceneManagement;

public class CenasManager : MonoBehaviour
{
    private string sceneName = "nada";
    void Awake()
    {
        Time.timeScale = 1f; // Resume the game
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
    }
    void Start()
    {
        Time.timeScale = 1f; // Resume the game
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Menu")
        {
            MusicManager.Instance.PlayMusic("Menu");
        }
        if (sceneName == "tutorial nave")
        {
            MusicManager.Instance.PlayMusic("CavernaTensa");
        }
        if (sceneName == "Cenario 01") ;
        {
            MusicManager.Instance.PlayMusic("Cavernas");
        }
        if (sceneName == "Caverna_cenario 01") ;
        {
            MusicManager.Instance.PlayMusic("CavernaTensa");
        }
        if (sceneName == "Clima2")
        {
            MusicManager.Instance.PlayMusic("Cavernas");
        }
        if (sceneName == "Caverna2")
        {
            MusicManager.Instance.PlayMusic("CavernaTensa");
        }
        if (sceneName == "Futuro 1") ;
        {
            MusicManager.Instance.PlayMusic("MiniGame");
        }
        if (sceneName == "Futuro 2")
        {
            MusicManager.Instance.PlayMusic("Cavernas");
        }
        if (sceneName == "Caverna Tensa")
        {
            MusicManager.Instance.PlayMusic("CavernaTensa");
        }
        if (sceneName == "Futuro 4") ;
        {
            MusicManager.Instance.PlayMusic("Menu");
        }
    }

    // Update is called once per frame
    void Update()
        {

        }
}
