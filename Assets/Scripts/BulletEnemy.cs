using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem faiscas;
    private ParticleSystem faiscasParticleSystemInstance;
    void OnCollisionEnter2D(Collision2D collision)
    {       
        faiscasParticleSystemInstance = Instantiate(faiscas, transform.position, Quaternion.identity);
         Destroy(gameObject);
    }
}
