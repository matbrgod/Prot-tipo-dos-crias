using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healthEnemy;
    public float maxHealthEnemy = 3f;
    public float speed = 2f;
    public Transform player;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthEnemy = maxHealthEnemy;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }



    }

    public void TakeDamage(float damage)
    {
        healthEnemy -= damage;
        if (healthEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
           healthEnemy -= 1;// Assuming each bullet reduces health by 1
           if (healthEnemy <= 0)
            {
                Destroy(gameObject);
            }


        }
    }
}
   