using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class BotaoJogar : MonoBehaviour
{
    public void Play()
    {
            SceneManager.LoadScene("Cenario 01");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Game Over");
    }
    public void Leave()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
