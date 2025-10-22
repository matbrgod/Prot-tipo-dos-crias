using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CavernaPassado : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene("Laboratório 1");
            Debug.Log("Collision detected with" + collision.gameObject.name);
        }


    }
}