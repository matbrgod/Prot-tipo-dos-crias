using UnityEngine;
using UnityEngine.SceneManagement;

public class CavernaPassado : MonoBehaviour
{
    // Certifique-se de que o CircleCollider2D NÃO está marcado como "Is Trigger" no Inspector
    // E que pelo menos um dos objetos envolvidos possui Rigidbody2D

    public void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("Cima2");
        Debug.Log("Collision detected with " + collision.gameObject.name);
    }
}