using UnityEngine;
using UnityEngine.SceneManagement;
 
public class PlayerMovement : MonoBehaviour
{
    public int health = 100;
    public int damage = 10;
    public float attackRange = 1.5f;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
 
    Vector2 movement;

    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Collider2D collision = Physics2D.OverlapCircle(transform.position, 0.5f, 128);
        if (collision)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Trap"))
            {
                health -= 100; // Diminui 10 de vida
                SceneManager.LoadScene("Menu");
                //Destroy(collision.gameObject); // Opcional
            }
        }
    }

        void FixedUpdate()
        {
            // Movement
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    
}