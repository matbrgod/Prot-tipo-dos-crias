using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaParaOPassado : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //SceneManager.LoadScene("Cima2");
            LevelManager.Instance.LoadScene("Cima2", "CrossFade");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }


    }
}
