using UnityEngine;
using UnityEngine.SceneManagement;
public class PortaParaOFuturo : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //SceneManager.LoadScene("Futuro 1");
            LevelManager.Instance.LoadScene("Futuro1", "CrossFade");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }


    }
}
