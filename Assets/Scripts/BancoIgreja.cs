using UnityEngine;

public class BancoIgreja : MonoBehaviour
{
    [SerializeField] private Transform localQuebrado1;
    [SerializeField] private Transform localQuebrado2;
    [SerializeField] private GameObject bancoQuebrado1;
    [SerializeField] private GameObject bancoQuebrado2;
    public bool quebravel;

    private void Start()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (quebravel == true)
            {
                Instantiate(bancoQuebrado1, localQuebrado1.transform.position, gameObject.transform.rotation);
                Instantiate(bancoQuebrado2, localQuebrado2.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
