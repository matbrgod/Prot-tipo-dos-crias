using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
using FirstGearGames.SmoothCameraShaker;
public class BossIgreja : MonoBehaviour
{
    private Camera camera;
    public ShakeData explosionShakeData;
    public int healthEnemy;
    public int maxHealthEnemy = 20;
    public float speed;
    public GameObject player;
    public float distanceBetween;

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
    public Transform firePoint2;
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

    //Sistema de Explosão
    [SerializeField] private ParticleSystem explosao;
    private ParticleSystem explosaoParticleSystemInstance;
    [SerializeField] private GameObject explosaoPrefab;
    public Transform localDeExplosao1;
    public Transform localDeExplosao2;
    public Transform localDeExplosao3;
    public Transform localDeExplosao4;
    public Transform[] PatrolPoints;
    private int currentPatrolIndex = 0;
    public GameObject bancoQBloqueiaSaida;
    public Collider2D circuloDeDeteccao;
    public GameObject portaFuturo4;
    public float ataqueEspecial;
    public Transform pontoDeEXPLOSAO;
    private bool kaBOOM = false;
    public GameObject musica;
    

    // Projeteis da Planta
    private float tempoUltimoTiro;
    public float tempoEntreTiros = 2f;
    public GameObject projetilPrefab;
    public Transform pontoDeDisparoProjetil;

    //local da Cutscene
    public GameObject cutscene;
    //Barra de vida
    public HealthBar healthBar;
    public GameObject vidaDoBoss;
    public GameObject quest; //barra de quest desabilita no fim da batalha

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
        healthBar.SetMaxHealth(maxHealthEnemy);
    }
    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        HomemBomba();

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
            Atacar();
        }
        if (healthEnemy == maxHealthEnemy / 2 | healthEnemy == maxHealthEnemy / 4)
        {
            patrulhando = false;
            kaBOOM = true;
            //HomemBomba();
        }
        if (kaBOOM == false)
        {
            espera += Time.deltaTime;
            if (healthEnemy <= 20 & healthEnemy >= 16)
            {
                tempoEntreTiros = 3f;
                if (espera >= tempoDeEspera + 30)
                {
                    Explosao(localDeExplosao1);
                    Explosao(localDeExplosao2);
                    espera = 0f;
                }
            }
            if (healthEnemy <= 15 & healthEnemy >= 11)
            {
                tempoEntreTiros = 2f;
                if (espera >= tempoDeEspera + 20)
                {
                    Explosao(localDeExplosao1);
                    Explosao(localDeExplosao2);
                    espera = 0f;
                }
            }
            if (healthEnemy <= 10 & healthEnemy >= 3)
            {
                tempoEntreTiros = 1f;
                espera += Time.deltaTime;
                if (espera >= tempoDeEspera + 10)
                {
                    Explosao(localDeExplosao1);
                    Explosao(localDeExplosao2);
                    //Explosao(localDeExplosao1);
                    //Explosao(localDeExplosao2);
                    espera = 0f;
                }
            }
            if(healthEnemy <= 2)
            {
                ataqueEspecial += Time.deltaTime;
                if (ataqueEspecial >0.5f & ataqueEspecial < 1.5f)
                {
                    tempoEntreTiros = 0.1f;
                }
                if (ataqueEspecial >= 1.6f)
                {
                    tempoEntreTiros = 1f;
                }
                if (ataqueEspecial >= 20f)
                {
                    ataqueEspecial = 0f;
                }
                    if (espera >= tempoDeEspera)
                    {
                        Explosao(localDeExplosao1);
                        Explosao(localDeExplosao2);
                        //Explosao(localDeExplosao1);
                        //Explosao(localDeExplosao2);
                        espera = 0f;
                    }
            }
        }
    }

    /*void FixedUpdate()
    {
        if (mirar)
        {
            Vector2 aimDirection = (Vector2)playerPosition.position - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
    }*/

    void HomemBomba()
    {
        if (kaBOOM == true)
        {
            patrulhando = false;
            speed = 7f;
            tempoEntreTiros = 0.1f;
            agent.SetDestination(pontoDeEXPLOSAO.position);
            if (!agent.pathPending && agent.remainingDistance < 1f)
            {
                Explosao(transform);
                Explosao(localDeExplosao1);
                Explosao(localDeExplosao2);
                Explosao(localDeExplosao1);
                Explosao(localDeExplosao2);
                healthEnemy -= 1;
                kaBOOM = false;
                patrulhando = true;
            }
        }
    }

    void Atacar()
    {
        if (Time.time - tempoUltimoTiro >= tempoEntreTiros)
        {
            Instantiate(projetilPrefab, transform.position, Quaternion.identity);
            tempoUltimoTiro = Time.time;
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
                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    //currentPatrolIndex = (currentPatrolIndex + 1) % PatrolPoints.Length;
                    Random random = new Random();
                    int randomPointPatrulha = random.Next(PatrolPoints.Length);
                    currentPatrolIndex = randomPointPatrulha;
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
        armaQGira.mirar = true;

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
        SoundManager.Instance.PlaySound2D("Tiros");
    }

    void SpawnParticlesSangue()
    {
        sangueParticleSystemInstance = Instantiate(sangue, transform.position, Quaternion.identity);
    }
    void Explosao(Transform local)
    {
        //explosaoParticleSystemInstance = Instantiate(explosao, transform.position, Quaternion.identity);
        /*if (local == null)
        {
            Instantiate(explosaoPrefab, transform.position, Quaternion.identity);
        }
        if (local != null)
        {
            Instantiate(explosaoPrefab, local.transform.position, Quaternion.identity);
        }*/
        Instantiate(explosaoPrefab, local.transform.position, Quaternion.identity);

        Collider2D[] objetos = Physics2D.OverlapCircleAll(local.transform.position, areaDeImpacto);
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
                CameraShakerHandler.Shake(explosionShakeData);
            }
        }
    }

    public void TakeDamage(/*float damage*/)
    {

        healthEnemy -= /*damage*/1;
        SpawnParticlesSangue();
        if (healthBar != null) healthBar.SetHealth(healthEnemy);
        if (healthEnemy <= 0)
        {
            Explosao(transform);
            camera.orthographicSize = 5f;
            portaFuturo4.SetActive(true);
            MusicManager.Instance.PlayMusic("Cavernas");
            if (vidaDoBoss != null) vidaDoBoss.SetActive(false); else Debug.Log("vidaDoBoss is null");
            if (quest != null) quest.SetActive(false); else Debug.Log("quest is null");
            Destroy(gameObject);
        }
    }

    public void HoraDoDuelo()
    {
        detectado = true;
        Destroy(cutscene);
        circuloDeDeteccao.enabled = false;
        MusicManager.Instance.PlayMusic("BossIgreja");
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
            HoraDoDuelo();
        }
    }
}