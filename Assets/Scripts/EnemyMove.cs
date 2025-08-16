using UnityEngine;

public class EnemyMove : MonoBehaviour



{
    public int health = 100;
    public int damage = 10;
    public float attackRange = 1.5f;
    public float speed = 2f;
    public Transform player;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}