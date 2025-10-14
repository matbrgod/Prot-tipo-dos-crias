using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform posicaoInicialDoPlayer;
    public int limiteDeFPS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Application.targetFrameRate = limiteDeFPS;

        Instantiate(playerPrefab, posicaoInicialDoPlayer.position, Quaternion.identity);
    }
    public void SavePlayerHealth(float playerHealthToSave)
    {
        PlayerPrefs.SetFloat("Health", playerHealthToSave);
    }
    public float LoadPlayerHealth()
    {
        return PlayerPrefs.GetFloat("Health");
    }
    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Gameplay Level 2");
    }
}

