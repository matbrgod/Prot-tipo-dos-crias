using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class BotaoJogar : MonoBehaviour
{
    //[SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject Error;
    public GameObject yesButton;
    public GameObject noButton;

    public void Play()
    {
        if (Error != null)
            Error.SetActive(false);
        
        if (yesButton != null)
            yesButton.SetActive(false);
        if (noButton != null)
            noButton.SetActive(false);    
        SceneManager.LoadScene("tutorial nave");
        //StartCoroutine(systemLoading());
            
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
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("tutorial nave");
    }
}
