using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaTutorial : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Intro");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }


    }
}
