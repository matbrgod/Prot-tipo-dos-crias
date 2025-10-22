using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class BotaoJogar : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject Error;
    public void Play()
    {
        if (Error != null)
            Error.SetActive(false);
        if (loadingScreen != null)
            loadingScreen.SetActive(true);
        StartCoroutine(systemLoading());
            
    }
    public void Credits()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void Leave()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator systemLoading()
    {
        yield return new WaitForSeconds(2.67f);
        SceneManager.LoadScene("tutorial nave");
    }
}
