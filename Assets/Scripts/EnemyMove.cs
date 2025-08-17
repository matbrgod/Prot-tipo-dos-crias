using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int healthEnemy;
    public float speed = 2f;
    public Transform player;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthEnemy = 30;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        if(healthEnemy <= 0)
            {
                Destroy(gameObject);
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            healthEnemy -= 10; // Assuming each bullet reduces health by 10
            if(healthEnemy <= 0)
            {
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}