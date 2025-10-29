using UnityEngine;
using UnityEngine.AI;

public class BossIgreja : MonoBehaviour
{
    private Camera camera;
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
    private bool patrulhando = true;
    private float espera = 0f;
    public float tempoDeESpera = 2f;
    private bool continuarPatrulha = true;
    private NavMeshAgent agent;
    [SerializeField] private ParticleSystem sangue;
    private ParticleSystem sangueParticleSystemInstance;
    public Transform[] PatrolPoints;
    private int currentPatrolIndex = 0;
    public GameObject bancoQBloqueiaSaida;
    public planta planta;
    public GameObject portaFuturo4;

    void Start()
    {
        camera = Camera.main;
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
            //Se o Player for detectado o Boss Come�a a disparar
            camera.orthographicSize = 7f;
            bancoQBloqueiaSaida.SetActive(true);
            Fight();
            Patrulha();
        }

        if (healthEnemy < 7)
        {
            planta.tempoEntreTiros = 1f;
        }
        if(healthEnemy < 1)
        {
            planta.tempoEntreTiros = 0.1f;
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

    private void Fight()
    {
        MirandoEAtirando();
    }

    private void Patrulha()
    {
        if (patrulhando == true)
        {
            agent.speed = speed;
            if (PatrolPoints != null && PatrolPoints.Length > 0)
            {
                agent.SetDestination(PatrolPoints[currentPatrolIndex].position);
                if (!agent.pathPending && agent.remainingDistance < 2f)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % PatrolPoints.Length;
                    agent.SetDestination(PatrolPoints[currentPatrolIndex].position);
                    //Tempo de espera improvisado pq n�o d� pra usar WaitForSeconds em fun��o e eu sou burro
                    /*continuarPatrulha = false;
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
                    }*/
                }
            }
        }
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 90));
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
            camera.orthographicSize = 5f;
            planta.tempoEntreTiros = 2f;
            portaFuturo4.SetActive(true);
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