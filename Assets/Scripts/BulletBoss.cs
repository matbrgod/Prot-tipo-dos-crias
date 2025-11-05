using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public float areaDeImpacto;
    public float force;
    [SerializeField] private GameObject explosaoPrefab;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Explosao();
        Destroy(gameObject);

    }

    void Explosao()
    {
        Instantiate(explosaoPrefab, transform.position, Quaternion.identity);
        
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, areaDeImpacto);
        foreach (Collider2D colisor in objetos)
        {
            Rigidbody2D rb2D = colisor.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direction = colisor.transform.position - transform.position;
                direction.Normalize();
                colisor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                colisor.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
                float distancia = 1 + direction.magnitude;
                float forceFinal = force / distancia;
                if (colisor.CompareTag("Player"))
                {
                    rb2D.AddForce(direction * forceFinal);
                }
                if (colisor.CompareTag("Boss"))
                {
                    continue;
                }
                else if (!colisor.CompareTag("Boss"))
                {
                    rb2D.AddForce(direction * forceFinal * 2, ForceMode2D.Impulse);
                }
            }
        }
    }
}
