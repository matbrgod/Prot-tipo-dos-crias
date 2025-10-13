using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed = 0f;
    public float healthEnemy;
    public float maxHealthEnemy = 3f;
    void Start()
    {
        targetPoint = 0;
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
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    void IncreaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
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
}
