using UnityEngine;
using UnityEngine.AI;

public class EnemyAtirador : MonoBehaviour
{
    public float healthEnemy;
    public float maxHealthEnemy = 3f;
    public float speed;
    public GameObject player;
    public float distanceBetween;
    public AudioSource tiro;
    [Range(0.5f, 2f)] public float firePitchMin = 0.7f;
    [Range(0.5f, 2f)] public float firePitchMax = 1.3f;

    private float distance;
    private Rigidbody2D rb;
    //private bool mirar = false;
    public ArmaQGira armaQGira;
    private float fireCooldown = 1;
    private float fireTimer = 0f;
    Vector2 moveDirection;
    private Transform playerPosition;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    private bool detectado = false;
    private bool patrulhando = true;
    private float espera = 0f;
    public float tempoDeESpera = 2f;
    private bool continuarPatrulha = true;
    private NavMeshAgent agent;
    [SerializeField] private ParticleSystem sangue;
    private ParticleSystem sangueParticleSystemInstance;
    public Transform[] PatrolPoints;
    private int currentPatrolIndex = 0;

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


        if (detectado == false)
        {
            //Se o Player n�o for detectado o inimigo patrulha
            Patrulha();
        }

        if (PatrolPoints == null || PatrolPoints.Length == 0)
        {
            detectado = true;
        }

        if (detectado == true)
        {
            //Se o Player for detectado o inimigo come�a a perseguir
            patrulhando = false;
            Perseguicao();
            armaQGira.mirar = true;
        }
    }

    private void Patrulha()
    {
        if (patrulhando == true)
        {
            agent.speed = speed / 2;
            if (PatrolPoints != null && PatrolPoints.Length > 0)
            {
                agent.SetDestination(PatrolPoints[currentPatrolIndex].position);
                if (!agent.pathPending && agent.remainingDistance < 2f)
                {
                    //Tempo de espera improvisado pq n�o d� pra usar WaitForSeconds em fun��o e eu sou burro
                    continuarPatrulha = false;
                    espera += Time.deltaTime;
                    if (espera >= tempoDeESpera)
                    {
                        continuarPatrulha = true;
                        espera = 0f;
                    }
                    if (continuarPatrulha == true)
                    {
                        currentPatrolIndex = (currentPatrolIndex + 1) % PatrolPoints.Length;
                        agent.SetDestination(PatrolPoints[currentPatrolIndex].position);
                    }
                }
            }
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
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        agent.SetDestination(player.transform.position);
        agent.speed = speed;
        //transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, angle - 90f);
    }
    public void MirandoEAtirando(float cooldown = 0)
    {

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
        if (tiro != null)
        {
            tiro.pitch = Random.Range(firePitchMin, firePitchMax);
            tiro.Play();
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0,0,90));
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
        if (collision.CompareTag("Player") | collision.CompareTag("Enemy"))
        {
            detectado = true;
        }
    }
}
   