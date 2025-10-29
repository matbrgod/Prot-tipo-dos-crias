using UnityEngine;

public class BancoIgreja : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet do Boss"))
        {
            Destroy(gameObject);
        }
    }
}
