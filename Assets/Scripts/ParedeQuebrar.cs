using UnityEngine;
using System.Collections;

public class ParedeQuebrar : MonoBehaviour
{
    [Header("Configurações da Parede")]
    [SerializeField] private float vida = 3f;
    [SerializeField] private float vidaMaxima = 3f;
    [SerializeField] private GameObject objetoSubstituto;

    private Renderer rend;
    private Color corOriginal;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            corOriginal = rend.material.color;
        vida = vidaMaxima;
    }

    // Para colisão 2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("Melee"))
        {
            this.SofrerDano(1f);
        }
    }

    public void TakeDamage(float damage)
    {
        this.SofrerDano(damage);
    }

    private void SofrerDano(float dano)
    {
        vida -= dano;
        if (rend != null)
            StartCoroutine(Piscar());

        if (vida <= 0)
        {
            DestruirParede();
        }
    }

    private IEnumerator Piscar()
    {
        rend.material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = corOriginal;
    }

    private void DestruirParede()
    {
        if (objetoSubstituto != null)
        {
            Instantiate(objetoSubstituto, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
