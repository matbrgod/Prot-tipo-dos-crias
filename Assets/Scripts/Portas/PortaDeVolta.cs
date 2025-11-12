using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaDeVolta : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //SceneManager.LoadScene("Cenario 01");
            LevelManager.Instance.LoadScene("Cenario 01", "CrossFade");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }


    }
}
