using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float healthEnemy;
    public float maxHealthEnemy;
    public float speed;
    public GameObject player;
    public float distanceBetween;
    public Animator enemyAnimator;
    private Transform playerPosition;
    private float distance;
    private float espera = 0f;
    public float tempoDeESpera = 2f;
    public float walkThreshold = 0.05f;
     private Vector3 lastPosition;
    private bool continuarPatrulha = true;
    private NavMeshAgent agent;

    private Rigidbody2D rb;
    private bool detectado = false;
    private bool patrulhando = true;
    public Transform[] PatrolPoints;
    private int currentPatrolIndex = 0;

    
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthEnemy = maxHealthEnemy;
        player = GameObject.FindWithTag("Player");
        playerPosition = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        lastPosition = transform.position;
       
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // determine current speed: prefer NavMeshAgent velocity, fallback to position delta
        float currentSpeed = 0f;
        if (agent != null)
        {
            // ensure agent is moving the transform
            agent.updatePosition = true;
            // use agent.velocity (Vector3) magnitude
            currentSpeed = agent.velocity.magnitude;
        }
        else
        {
            float movedThisFrame = Vector2.Distance(transform.position, lastPosition);
            currentSpeed = movedThisFrame / Mathf.Max(Time.deltaTime, 0.0001f);
        }

        bool isWalking = currentSpeed > walkThreshold;

        if (enemyAnimator != null && enemyAnimator.GetBool("IsWalking") != isWalking)
            enemyAnimator.SetBool("IsWalking", isWalking);

        lastPosition = transform.position;

        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, -180, 0); // Facing right
        else if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Facing left

        if (detectado == false)
        {
            //Se o Player não for detectado o inimigo patrulha
            Patrulha();
        }

        if (PatrolPoints == null || PatrolPoints.Length == 0)
            {
                detectado = true;
            }

        if (detectado == true)
        {
            //Se o Player for detectado o inimigo começa a perseguir
            patrulhando = false;
            Perseguindo();
        }
    }

    private void Patrulha()
    {
        if (patrulhando == true)
        {
            agent.speed = speed /2;
            if (PatrolPoints != null && PatrolPoints.Length > 0)
            {
                agent.SetDestination(PatrolPoints[currentPatrolIndex].position);
                if (!agent.pathPending && agent.remainingDistance < 2f)
                {
                    //Tempo de espera improvisado pq não dá pra usar WaitForSeconds em função e eu sou burro
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
    private void Perseguindo()
    {
        agent.destination = player.transform.position;
        if (distance > distanceBetween)
        {
            //O inimigo vai atrás do player
            IrAtras();
        }
        if (distance <= distanceBetween)
        {
            IrAtras();//Por enquanto o bixo vai dar dano encostando mesmo
            //Atacar();
        }
    }
    private void IrAtras()
    {
        //Movimenta o inimigo atrás do player usando NavMesh
        agent.SetDestination(player.transform.position);
        agent.speed = speed;
    }
    
    private void Atacar()
    {
        //O inimigo ata uma area próxima para causar dano
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
        //Se o inimigo colidir com o tiro do player ele perde vida
        if (collision.collider.CompareTag("Bullet"))
        {
            TakeDamage(1f);
        }

        if(collision.collider.CompareTag("Player"))
        {
            //Se o inimigo bater no payer o inimigo recua um pouco
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Se O Player ou Tiro do Player entrar na área de Agro do Inimigo ele vai perceber
        if (collision.CompareTag("Player") | collision.CompareTag("Bullet"))
        {
            detectado = true;
        }
    }
}