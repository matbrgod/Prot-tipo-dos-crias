using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta_Caverna_Cenario01 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene("Caverna_cenario 01");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }
    }
}
