using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta_Caverna_Cenario02 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene("Caverna2");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }


    }
}
