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
    public float tempoDeEspera;
    public float areaDeImpacto;
    public float force;
    private bool continuarPatrulha = true;
    private NavMeshAgent agent;
    [SerializeField] private ParticleSystem sangue;
    private ParticleSystem sangueParticleSystemInstance;
    [SerializeField] private ParticleSystem explosao;
    private ParticleSystem explosaoParticleSystemInstance;
    public Transform[] PatrolPoints;
    private int currentPatrolIndex = 0;
    public GameObject bancoQBloqueiaSaida;
    public planta planta;
    public GameObject portaFuturo4;
    public float ataqueEspecial;
    public Transform pontoDeEXPLOSAO;
    private bool kaBOOM = false;
    public Transform explosaoExtra1;
    public Transform explosaoExtra2;

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


        //if (detectado == false)
        {
            //Se o Player n�o for detectado o inimigo patrulha
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
        if (healthEnemy == maxHealthEnemy / 2 | healthEnemy == maxHealthEnemy / 4)
        {
            patrulhando = false;
            kaBOOM = true;
            HomemBomba();
        }
        if (kaBOOM == false)
        {
            if (healthEnemy <= 20 & healthEnemy >= 16)
            {
                planta.tempoEntreTiros = 3f;
            }
            if (healthEnemy <= 15 & healthEnemy >= 11)
            {
                planta.tempoEntreTiros = 2f;
            }
            if (healthEnemy <= 10 & healthEnemy >= 3)
            {
                planta.tempoEntreTiros = 1f;
            }
            if(healthEnemy <= 2)
            {
                ataqueEspecial += Time.deltaTime;
                if (ataqueEspecial >0.5f & ataqueEspecial < 1.5f)
                {
                    planta.tempoEntreTiros = 0.1f;
                }
                if (ataqueEspecial >= 1.6f)
                {
                    planta.tempoEntreTiros = 1f;
                }
                if (ataqueEspecial >= 20f)
                {
                    ataqueEspecial = 0f;
                }
                espera += Time.deltaTime;
                    if (espera >= tempoDeEspera)
                    {
                        ExplosaoExtra1();
                        ExplosaoExtra2();
                        espera = 0f;
                    }
            }
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

    void HomemBomba()
    {
        if (kaBOOM == true)
        {
            patrulhando = false;
            speed = 7f;
            planta.tempoEntreTiros = 0.1f;
            agent.SetDestination(pontoDeEXPLOSAO.position);
            if (!agent.pathPending && agent.remainingDistance < 1f)
            {
                Explosao();
                ExplosaoExtra1();
                ExplosaoExtra2();
                ExplosaoExtra1();
                ExplosaoExtra2();
                healthEnemy -= 1;
                kaBOOM = false;
                patrulhando = true;
            }
        }
    }

    private void Fight()
    {
        if(kaBOOM == false)
        {
            MirandoEAtirando();
        }
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
    void Explosao()
    {
        explosaoParticleSystemInstance = Instantiate(explosao, transform.position, Quaternion.identity);

        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, areaDeImpacto);
        foreach (Collider2D colisor in objetos)
        {
            Rigidbody2D rb2D = colisor.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direction = colisor.transform.position - transform.position;
                direction.Normalize();
                colisor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                colisor.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
                float distancia = 1 + direction.magnitude;
                float forceFinal = force / distancia;
                rb2D.AddForce(direction * forceFinal * 2, ForceMode2D.Impulse);
                if (colisor.CompareTag("Player"))
                {
                    rb2D.AddForce(direction * forceFinal);
                }
            }
        }
    }
    void ExplosaoExtra1()
    {
        explosaoParticleSystemInstance = Instantiate(explosao, explosaoExtra1.transform.position, Quaternion.identity);

        Collider2D[] objetos = Physics2D.OverlapCircleAll(explosaoExtra1.transform.position, areaDeImpacto);
        foreach (Collider2D colisor in objetos)
        {
            Rigidbody2D rb2D = colisor.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direction = colisor.transform.position - explosaoExtra1.transform.position;
                direction.Normalize();
                colisor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                colisor.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
                float distancia = 1 + direction.magnitude;
                float forceFinal = force / distancia;
                rb2D.AddForce(direction * forceFinal * 2, ForceMode2D.Impulse);
                if (colisor.CompareTag("Player"))
                {
                    rb2D.AddForce(direction * forceFinal);
                }
            }
        }
    }
    void ExplosaoExtra2()
    {
        explosaoParticleSystemInstance = Instantiate(explosao, explosaoExtra2.transform.position, Quaternion.identity);
        
        Collider2D[] objetos = Physics2D.OverlapCircleAll(explosaoExtra2.transform.position, areaDeImpacto);
        foreach (Collider2D colisor in objetos)
        {
            Rigidbody2D rb2D = colisor.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direction = colisor.transform.position - explosaoExtra2.transform.position;
                direction.Normalize();
                colisor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                colisor.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
                float distancia = 1 + direction.magnitude;
                float forceFinal = force / distancia;
                rb2D.AddForce(direction * forceFinal * 2, ForceMode2D.Impulse);
                if (colisor.CompareTag("Player"))
                {
                    rb2D.AddForce(direction * forceFinal);
                }
            }
        }
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
        if (collision.collider.CompareTag("Bullet"))
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