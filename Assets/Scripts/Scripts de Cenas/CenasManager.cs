using UnityEngine;
using UnityEngine.SceneManagement;

public class CenasManager : MonoBehaviour
{
    //private string sceneName = "nada";
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Time.timeScale = 1f; // Resume the game
        Debug.Log("The active scene is: " + currentScene.name);
        switch (sceneName)
        {
            case "tutorial nave":
            {
                MusicManager.Instance.PlayMusic("CavernaTensa");
                break;
            }
            case "Menu":
            {
                MusicManager.Instance.PlayMusic("Menu");
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
                case "Caverna Tensa":
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
