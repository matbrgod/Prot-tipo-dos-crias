using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pauseMenu;
    public GameObject WeaponParent;
    void Awake()
    {
        Time.timeScale = 1f; // Ensure the game is running at normal speed when starting
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CanvaPause();
        }
    }
    public void CanvaPause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
        WeaponParent.SetActive(!WeaponParent.activeSelf);
    }
}
