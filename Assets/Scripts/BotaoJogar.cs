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
        LevelManager.Instance.LoadScene("tutorial nave", "CrossFade");
        //StartCoroutine(systemLoading());
            
    }
    public void Credits()
    {
        MusicManager.Instance.PlayMusic("Parar");
        LevelManager.Instance.LoadScene("Creditos", "CrossFade");
    }
    public void Leave()
    {
        Application.Quit();
    }
    public void Menu()
    {
        LevelManager.Instance.LoadScene("Menu", "CrossFade");
    }

    private IEnumerator systemLoading()
    {
        yield return new WaitForSeconds(2.5f);
        LevelManager.Instance.LoadScene("tutorial nave", "CrossFade");
    }
}
