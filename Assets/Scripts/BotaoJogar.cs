using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class BotaoJogar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
        
            SceneManager.LoadScene("Level1");

        }

    }
}
