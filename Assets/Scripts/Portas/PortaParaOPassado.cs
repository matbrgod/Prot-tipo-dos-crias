using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaParaOPassado : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Cima2");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }


    }
}
