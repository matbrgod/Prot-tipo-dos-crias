using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;

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

    private float triggerTickTimer = 0f;
    public float triggerTickInterval = 1f;
    private float originalMoveSpeed;
    public Animator animator;
    private bool isInvincible;                 
    public float invincibleDuration;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject reloadingUI;
    [SerializeField] private GameObject efeitoTiro;
    [SerializeField] private ParticleSystem sangue;
    private ParticleSystem sangueParticleSystemInstance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        healthPlayer = maxHealthPlayer;
        originalMoveSpeed = moveSpeed;
        healthText.text = "" + healthPlayer;
        isInvincible = false;

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (enemyLayer != -1)
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);
    }

    void Update()
    {
        // Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        healthText.text = "" + healthPlayer;

        if(moveX != 0 || moveY != 0)
        animator.SetBool("EstaAndando", true);
        else
        animator.SetBool("EstaAndando", false);

        if (healthPlayer <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }

        timerTiro += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timerTiro >= tiroCooldown)
        {
            timerTiro = 0f;
            weapon.Fire();
            

        }

        if (reloadingUI != null)
            reloadingUI.SetActive(timerTiro < tiroCooldown);

        if(efeitoTiro != null)
            efeitoTiro.SetActive(timerTiro < 0.2f);
            

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

        
    }

    void OnCollisionEnter2D(Collision2D collision)
        {            
                if (collision.collider.CompareTag("Enemy"))
                {
                    healthPlayer -= 10; // Diminui 10 de vida
                    SpawnParticlesSangue();
                    healthText.text = "" + healthPlayer;
                    
                    
                    StartCoroutine(InvincibilityCoroutine());
                    
                    if (healthPlayer <= 0)
                    {   
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
                StartCoroutine(InvincibilityCoroutine());
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

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        int playerLayer = gameObject.layer;
        int enemyLayer = LayerMask.NameToLayer("Enemy"); // ensure your enemies use this layer
        if (enemyLayer != -1)
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        // optional visual feedback
        if (spriteRenderer != null) spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(invincibleDuration);

        if (enemyLayer != -1)
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);

        isInvincible = false;
        if (spriteRenderer != null) spriteRenderer.color = Color.white;
    }

    /*private IEnumerator EfeitoTiroCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        efeitoTiro.SetActive(false);
    }*/

    private void OnDisable()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (enemyLayer != -1)
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);

        // stop any running invincibility coroutine and reset visuals/state
        StopAllCoroutines();
        isInvincible = false;
        if (spriteRenderer != null) spriteRenderer.color = Color.white;
    }

    private void OnDestroy()
    {
        // same safety on destroy
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (enemyLayer != -1)
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);
    }


    void FixedUpdate()
    {
        // Movement
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        //Vector2 aimDirection = mousePosition - rb.position;
        //float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        // rb.rotation = aimAngle;
       

    }

    void SpawnParticlesSangue()
    {
        sangueParticleSystemInstance = Instantiate(sangue, transform.position, Quaternion.identity);
    }



}