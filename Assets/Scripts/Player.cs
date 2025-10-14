using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;

public class Player : MonoBehaviour
{
    public int healthPlayer;

    public int maxHealthPlayer = 100;
    //public int damage = 10;
    //public float attackRange = 1.5f;
    public TMP_Text healthText;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    public Weapon weapon;
 
    Vector2 moveDirection;

    Vector2 mousePosition;
    public WeaponParent WeaponParent;
    
    public bool interact = false;
    public GameManager gameManager;
    
    SpriteRenderer spriteRenderer;

    void Start()
    {
        healthPlayer = maxHealthPlayer;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
      
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            WeaponParent.Attack();            
        }
        
        interact = Input.GetKeyDown(KeyCode.E);

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouseWorldPos.x > transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else 
            transform.rotation = Quaternion.Euler(0, -180, 0);

        healthText.text = "Health: " + healthPlayer;    
        
    }

    void OnCollisionEnter2D(Collision2D collision)
        {            
                if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Veneno"))
                {
                    healthPlayer -= 10; // Diminui 10 de vida
                    if (healthPlayer <= 0)
                    {
                        SceneManager.LoadScene("Menu"); 
                    }
                    //Destroy(collision.gameObject); // Opcional
                }
            
        }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Veneno"))
        {
            healthPlayer -= 10; // Diminui 10 de vida
            if (healthPlayer <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
            //Destroy(collision.gameObject); // Opcional
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        //Vector2 aimDirection = mousePosition - rb.position;
        //float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        // rb.rotation = aimAngle;
       

    }

    
    
}