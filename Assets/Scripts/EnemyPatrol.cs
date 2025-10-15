using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    public int healthEnemy;
    public int maxHealthEnemy;

    public HealthBar healthBar;
    
    void Start()
    {
        targetPoint = 0;
        healthBar.SetMaxHealth(maxHealthEnemy);
        healthEnemy = maxHealthEnemy;
        
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
        Vector2 direction = patrolPoints[targetPoint].position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);

        // Flip enemy depending on movement direction
        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, -180, 0); // Facing right
        else if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Facing left
    }

    void IncreaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
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
            healthEnemy -= 1;// Assuming each bullet reduces health by 1
            healthBar.SetHealth(healthEnemy);
           if (healthEnemy <= 0)
            {
                SceneManager.LoadScene("Intro");
                Destroy(gameObject);
            }


        }
    }
}
