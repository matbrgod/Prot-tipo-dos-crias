using UnityEngine;
using UnityEngine.SceneManagement;
public class BoosPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    public int healthEnemy;
    public int maxHealthEnemy;
    private bool atacar = false;
    public GameObject player;

    public HealthBar healthBar;
    public GameObject porta;
    public GameObject vidaDoBoss;
    public GameObject TriggerDoE;
    
    void Start()
    {
        targetPoint = 0;
        healthBar.SetMaxHealth(maxHealthEnemy);
        healthEnemy = maxHealthEnemy;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == patrolPoints[targetPoint].position)
        {
            IncreaseTargetInt();
        }
        {
            if (transform.position == patrolPoints[targetPoint].position)
            {
                targetPoint = 0;
            }
        }
        if (atacar == true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Vector2 direction = patrolPoints[targetPoint].position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
            // Flip enemy depending on movement direction
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

    //public void TakeDamage(int damage)
    //{
    //    healthEnemy -= damage;
//
 //       healthBar.SetHealth(healthEnemy);
  //      if (healthEnemy <= 0)
  //      {
   //         Destroy(gameObject);
      //  }
    //}
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            atacar = false;
            healthEnemy -= 1;// Assuming each bullet reduces health by 1
            healthBar.SetHealth(healthEnemy);
            if (healthEnemy <= 0)
            {
                //SceneManager.LoadScene("Intro");
                Destroy(gameObject);
                porta.SetActive(false);
                vidaDoBoss.SetActive(false);
                TriggerDoE.SetActive(true);
            }


        }
        if (collision.collider.CompareTag("Player"))
        {
            atacar = false;
        }
    }
}
