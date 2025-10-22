using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healthEnemy;
    public float maxHealthEnemy = 3f;
    public float speed;
    //public GameObject player;
    public float distanceBetween;

    private float distance;

    private Rigidbody2D rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthEnemy = maxHealthEnemy;
        //player = GameObject.FindGameObjectWithTag("Player");
       
    }
    void Update()
    {

        distance = Vector2.Distance(transform.position, Player.instance.transform.position);
        Vector2 direction = Player.instance.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, -180, 0); // Facing right
        else if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Facing left

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.instance.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * (angle-180));
        }
    }

    void FixedUpdate()
    {



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