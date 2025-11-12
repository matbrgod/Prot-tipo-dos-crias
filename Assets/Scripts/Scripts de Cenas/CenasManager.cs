using UnityEngine;
using UnityEngine.SceneManagement;

public class CenasManager : MonoBehaviour
{
    //private string sceneName = "nada";
    public static CenasManager Instance;
    public string ultimaCenaCarregada;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
                    ultimaCenaCarregada = "Menu";
                    MusicManager.Instance.PlayMusic("Menu");
                    break;
                }
            case "tutorial nave":
                {
                    ultimaCenaCarregada = "tutorial nave";
                    MusicManager.Instance.PlayMusic("CavernaTensa");
                    break;
                }
            case "Cenario 01":
                {
                    ultimaCenaCarregada = "Cenario 01";
                    MusicManager.Instance.PlayMusic("Mundo1");
                    break;
                }
            case"Caverna_cenario 01":
                {
                    ultimaCenaCarregada = "Caverna_cenario 01";
                    MusicManager.Instance.PlayMusic("Caverna1");
                    break;
                }
            case "Cima2":
                {
                    ultimaCenaCarregada = "Cima2";
                    MusicManager.Instance.PlayMusic("Caverna2");
                    break;
                }
            case "Caverna2":
                {
                    ultimaCenaCarregada = "Caverna2";
                    MusicManager.Instance.PlayMusic("CavernaTensa");
                    break;
                }
            case "Futuro 1":
                {
                    ultimaCenaCarregada = "Futuro 1";
                    MusicManager.Instance.PlayMusic("TiroteioTenso");
                    break;
                }
            case "Futuro 2":
                {
                    ultimaCenaCarregada = "Futuro 2";
                    MusicManager.Instance.PlayMusic("TiroteioTenso");
                    break;
                }
                case "Futuro 3":
                {
                    ultimaCenaCarregada = "Futuro 3";
                    MusicManager.Instance.PlayMusic("Cavernas");
                    break;
                }
                case "Futuro 4":
                {
                    ultimaCenaCarregada = "Futuro 4";
                    MusicManager.Instance.PlayMusic("Final");
                    break;
                }
        }
    }

    // Update is called once per frame
    public void UltimaCenaCarregada()
    {
        print(ultimaCenaCarregada);
        if(ultimaCenaCarregada == null)
        {
            ultimaCenaCarregada = "Menu";
        }
        LevelManager.Instance.LoadScene(ultimaCenaCarregada, "CrossFade");
    }
}
