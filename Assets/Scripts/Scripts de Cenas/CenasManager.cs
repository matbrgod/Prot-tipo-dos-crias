using UnityEngine;
using UnityEngine.SceneManagement;

public class CenasManager : MonoBehaviour
{
    //private string sceneName = "nada";
    public static CenasManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnMyCustomSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnMyCustomSceneLoaded;
    }
    void OnMyCustomSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Time.timeScale = 1f; // Resume the game
        Debug.Log("The active scene is: " + currentScene.name);
        switch (sceneName)
        {
            case "Menu":
            {
                MusicManager.Instance.PlayMusic("Menu");
                break;
            }
            case "tutorial nave":
            {
                MusicManager.Instance.PlayMusic("CavernaTensa");
                break;
            }
            case "Cenario 01":
            {
                    MusicManager.Instance.PlayMusic("Cavernas");
                    break;
            }
            case"Caverna_cenario 01":
                {
                    MusicManager.Instance.PlayMusic("CavernaTensa");
                    break;
                }
            case "Clima2":
                {
                    MusicManager.Instance.PlayMusic("Cavernas");
                    break;
                }
            case "Caverna2":
                {
                    MusicManager.Instance.PlayMusic("CavernaTensa");
                    break;
                }
            case "Futuro 1":
                {
                    MusicManager.Instance.PlayMusic("MiniGame");
                    break;
                }
            case "Futuro 2":
                {
                    MusicManager.Instance.PlayMusic("Cavernas");
                    break;
                }
                case "Futuro3":
                {
                    MusicManager.Instance.PlayMusic("CavernaTensa");
                    break;
                }
                case "Futuro 4":
                {
                    MusicManager.Instance.PlayMusic("Menu");
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
