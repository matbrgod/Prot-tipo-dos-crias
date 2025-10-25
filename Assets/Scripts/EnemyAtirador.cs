using UnityEngine;

public class EnemyAtirador : MonoBehaviour
{
    public float healthEnemy;
    public float maxHealthEnemy = 3f;
    public float speed;
    public GameObject player;
    public float distanceBetween;

    private float distance;

    private Rigidbody2D rb;
    private bool atirar = false;
    private bool mirar = false;
    private float fireCooldown = 1;
    private float fireTimer = 0f;
    Vector2 moveDirection;
    private Transform playerPosition;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    private bool detectado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthEnemy = maxHealthEnemy;
        player = GameObject.FindWithTag("Player");
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        
        if(detectado == true)
        {
            Perseguicao();
        }
    }

    void FixedUpdate()
    {
        if (mirar)
        {
            Vector2 aimDirection = (Vector2)playerPosition.position - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
    }
    public void Perseguicao()
    {
        if (distance > distanceBetween)
        {
            Perseguir();
        }
        if (distance <= distanceBetween)
        {
            Atirando();
            CooldownTiro();
        }
        if (distance < distanceBetween /2 + 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, -speed * Time.deltaTime);
        }
    }
    public void Perseguir()
    {
        mirar = false;
        atirar = false;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, angle - 90f);
    }
    public void Atirando()
    {
        mirar = true;
        atirar = true;
    }

    public void Disparar()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
    public void CooldownTiro()
    {
        if (atirar == true)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireCooldown)
            {
                Disparar();
                fireTimer = 0f;
            }
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
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ^ collision.CompareTag("Bullet"))
        {
            detectado = true;
        }
    }
}
   