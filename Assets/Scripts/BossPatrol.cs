using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    public int healthEnemy;
    public int maxHealthEnemy;
    private bool atacar = false;
    public GameObject player;
    [SerializeField] private ParticleSystem sangue;
    private ParticleSystem sangueParticleSystemInstance;


    public HealthBar healthBar;
    [Header("Componentes para desativar/ativar ao morrer")]
    [SerializeField] private GameObject porta;
    [SerializeField] private GameObject vidaDoBoss;
    [SerializeField] private GameObject TriggerDoE;
    [SerializeField] private GameObject spawnRatinhos;
    [SerializeField] private GameObject musica;
    [SerializeField] private GameObject quest1;
    //[SerializeField] private AudioSource musicaAmbiente;
    public GameObject quest;

    void Start()
    {
        targetPoint = 0;
        healthEnemy = maxHealthEnemy;
        healthBar.SetMaxHealth(maxHealthEnemy);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (transform.position == patrolPoints[targetPoint].position)
            IncreaseTargetInt();

        if (atacar)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Vector2 direction = patrolPoints[targetPoint].position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);

            // flip using spriteRenderer if you have one; shown here using rotation as before
            if (direction.x > 0)
                transform.rotation = Quaternion.Euler(0, -180, 0); // Facing right
            else if (direction.x < 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);  // Facing left
        }
    }

    void IncreaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            atacar = true;
            targetPoint = 0;
        }
    }

    // centralize damage + death handling
    public void TakeDamage(int damage)
{
    SpawnParticlesSangue();
    healthEnemy -= damage;
    Debug.Log($"{name} took {damage} dmg, health now {healthEnemy} (object={gameObject.name})");
    if (healthBar != null) healthBar.SetHealth(healthEnemy);

    if (healthEnemy <= 0)
    {
        Die();
    }
}

private void Die()
{
    Debug.Log($"{name} Die() called on {gameObject.name}");

    if (porta != null) porta.SetActive(false); else Debug.Log("porta is null");
    if (vidaDoBoss != null) vidaDoBoss.SetActive(false); else Debug.Log("vidaDoBoss is null");
    if (TriggerDoE != null) TriggerDoE.SetActive(true); else Debug.Log("TriggerDoE is null");
    if (spawnRatinhos != null) spawnRatinhos.SetActive(false); else Debug.Log("spawnRatinhos is null");
    if (musica != null) musica.SetActive(false); else Debug.Log("musica is null");
    //if (musicaAmbiente != null) musicaAmbiente.Play(); else Debug.Log("alarme is null");
    MusicManager.Instance.PlayMusic("CavernaTensa");
    if (quest != null) quest.SetActive(true); else Debug.Log("quest is null");
    if (quest1 != null) quest1.SetActive(false); else Debug.Log("quest1 is null");


    Destroy(gameObject);
}

    // Use whichever callback matches your bullet (Collision or Trigger)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            atacar = false;
            TakeDamage(1);
        }
        if (collision.collider.CompareTag("Player"))
        {
            atacar = false;
        }
    }

    void SpawnParticlesSangue()
    {
        sangueParticleSystemInstance = Instantiate(sangue,transform.position, Quaternion.identity);
    }

    // If bullets are triggers, uncomment and use this instead:
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            atacar = false;
            TakeDamage(1);
        }
    }
    */
}