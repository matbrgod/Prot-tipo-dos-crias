using UnityEngine;

public class ArmaQGira : MonoBehaviour
{
    public bool mirar = false;
    public GameObject player;
    private Transform playerPosition;
    private Rigidbody2D rb;
    [SerializeField] private GameObject Boss;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Boss.transform.position;
    }

    void FixedUpdate()
    {
        if (mirar)
        {
            Mirar();
        }
    }

    private void Mirar()
    {
        Vector2 aimDirection = (Vector2)playerPosition.position - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
