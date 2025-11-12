using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem faiscas;
    private ParticleSystem faiscasParticleSystemInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            return;
        }
        else
        {
            faiscasParticleSystemInstance = Instantiate(faiscas, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
