using UnityEngine;
using UnityEngine.AI;

public class EnemyAtirador : MonoBehaviour
{
    public float healthEnemy;
    public float maxHealthEnemy = 3f;
    public float speed;
    public GameObject player;
    public float distanceBetween;

    private float distance;
    private Rigidbody2D rb;
    private bool mirar = false;
    private float fireCooldown = 1;
    private float fireTimer = 0f;
    Vector2 moveDirection;
    private Transform playerPosition;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    private bool detectado = false;
    private NavMeshAgent agent;
    [SerializeField] private ParticleSystem sangue;
    private ParticleSystem sangueParticleSystemInstance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthEnemy = maxHealthEnemy;
        player = GameObject.FindWithTag("Player");
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.destination = player.transform.position;
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
            IrAtras();
            MirandoEAtirando(2f);
        }
        if (distance <= distanceBetween)
        {
            MirandoEAtirando();
        }
        if (distance < distanceBetween /2 + 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, -speed * Time.deltaTime);
        }
    }
    public void IrAtras()
    {
        mirar = false;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        agent.SetDestination(player.transform.position);
        agent.speed = speed;
        transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, angle - 90f);
    }
    public void MirandoEAtirando(float cooldown = 0)
    {
        mirar = true;

        //cooldown do tiro
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireCooldown + cooldown)
        {
            Disparar();
            fireTimer = 0f;
        }
    }

    public void Disparar()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    void SpawnParticlesSangue()
    {
        sangueParticleSystemInstance = Instantiate(sangue, transform.position, Quaternion.identity);
    }

    public void TakeDamage(/*float damage*/)
    {

        healthEnemy -= /*damage*/1;
        SpawnParticlesSangue();
        if (healthEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet") | collision.collider.CompareTag("Bullet dos inimigos"))
        {
            TakeDamage();
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") | collision.CompareTag("Bullet"))
        {
            detectado = true;
        }
    }
}
   