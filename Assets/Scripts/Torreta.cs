using UnityEngine;

public class Torreta : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool atirar = false;
    private bool mirar = false;
    private float fireCooldown = 1;
    private float fireTimer = 0f;

    Vector2 moveDirection;
    private Transform playerPosition;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    

    // Update is called once per frame
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (atirar == true)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireCooldown)
            {
                Atirar();
                fireTimer = 0f;
            }
        }
    }
    
    public void Atirar()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                atirar = true;
                mirar = true;
            }
        }

    private void FixedUpdate()
    {
        //rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
        if (mirar)
        {
            Vector2 aimDirection = (Vector2)playerPosition.position - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg -90f;
            rb.rotation = aimAngle;
        }
    }
}
