using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem faiscas;
    private ParticleSystem faiscasParticleSystemInstance;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            return;
        }
        else
        {
            SpawnParticlesfaiscas();
            Destroy(gameObject);
        }

    }
    void SpawnParticlesfaiscas()
    {
        faiscasParticleSystemInstance = Instantiate(faiscas, transform.position, Quaternion.identity);
    }
}
