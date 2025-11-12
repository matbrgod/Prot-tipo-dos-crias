using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta_Caverna_Cenario01 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene("Caverna_cenario 01");
            //LevelManager.Instance.LoadScene("Caverna_cenario 01", "CrossFade");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }
    }
}
