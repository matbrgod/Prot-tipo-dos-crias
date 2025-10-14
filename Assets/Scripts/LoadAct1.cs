using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class LoadAct1 : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKeyDown)
        {
        
            SceneManager.LoadScene("Cenario 01");

        }

    }
}
