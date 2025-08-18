using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
 
public class PlayerMovement : MonoBehaviour
{
    public int healthPlayer = 100;
    //public int damage = 10;
    //public float attackRange = 1.5f;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    public Weapon weapon;
 
    Vector2 moveDirection;

    Vector2 mousePosition;

    void Update()
    {
        // Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

         if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);



        Collider2D collision = Physics2D.OverlapCircle(transform.position, 0.5f, 128);
        if (collision)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Trap"))
            {
                healthPlayer -= 100; // Diminui 10 de vida
                SceneManager.LoadScene("Menu");
                //Destroy(collision.gameObject); // Opcional
            }
        }
    }

        void FixedUpdate()
        {
            // Movement
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        rb.rotation = aimAngle;
        }
    
}