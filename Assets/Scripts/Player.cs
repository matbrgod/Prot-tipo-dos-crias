using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int healthPlayer;

    public int maxHealthPlayer = 100;
    //public int damage = 10;
    //public float attackRange = 1.5f;
    public TMP_Text healthText;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    public Weapon weapon;
    public float timerTiro = 0f;
    public float tiroCooldown = 1f;
 
    Vector2 moveDirection;

    Vector2 mousePosition;
    public WeaponParent WeaponParent;
    
    public bool interact = false;
    public GameManager gameManager;

    SpriteRenderer spriteRenderer;

    //public GameObject trigger;

    //public GameObject animacao;

    private float triggerTickTimer = 0f;
    public float triggerTickInterval = 1f;
    private float originalMoveSpeed;
    public Animator animator;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        healthPlayer = maxHealthPlayer;
        originalMoveSpeed = moveSpeed;
        healthText.text = "" + healthPlayer;
    }

    void Update()
    {
        // Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(moveX != 0 || moveY != 0)
        animator.SetBool("EstaAndando", true);
        else
        animator.SetBool("EstaAndando", false);

        if (healthPlayer <= 0)
        {
            SceneManager.LoadScene("Menu");
        }

        timerTiro += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timerTiro >= tiroCooldown)
        {
            timerTiro = 0f;
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

            

        if (healthPlayer <= 0)
                    {
                        SceneManager.LoadScene("Game Over"); 
                    }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
        {            
                if (collision.collider.CompareTag("Enemy"))
                {
                    healthPlayer -= 10; // Diminui 10 de vida
                    if (healthPlayer <= 0)
            {
                        healthText.text = "" + healthPlayer;
                        SceneManager.LoadScene("Game Over"); 
                    }
                    //Destroy(collision.gameObject); // Opcional
                }
            
        }



    private void OnTriggerStay2D(Collider2D objectThatStayed)
    {
        triggerTickTimer += Time.deltaTime;
        if (triggerTickTimer >= triggerTickInterval)
        {
            if (objectThatStayed.CompareTag("fio") || objectThatStayed.CompareTag("Veneno"))
            {
                healthPlayer -= 10;
                healthText.text = "" + healthPlayer;
            }
            triggerTickTimer = 0f;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("fio") || other.CompareTag("Veneno"))
        {
            moveSpeed = 2f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("fio") || other.CompareTag("Veneno"))
        {
            moveSpeed = originalMoveSpeed;
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