using UnityEngine;
using UnityEngine.UI;

public class Rocks : MonoBehaviour
{
    public GameObject objetoADrop; 
    public float vidaMaxima=5f;
    public float vidaAtual;

    public Slider barraDeVida; // para adicionar uma barra de vida futuramente (ignorar por enquanto)   

    void Start()
    {
        vidaAtual = vidaMaxima;
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
    }
    void Update()
    {
        if (vidaAtual <= 0)
        {
            Quebrar();
        }
    }

    public void ReceberDano(float damage)
    {
        vidaAtual -= damage;
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual;
            SoundManager.Instance.PlaySound2D("Picaretada"); //Nao ta tocando nao sei pq
        }

        if (vidaAtual <= 0)
        {
            Quebrar();
        }
    }

    public void Quebrar() // para dropar de 1 a 3 peda�os na posi��o em volta do minerio
    {
        int quantidade = Random.Range(1, 4); //randomizador de quantidade

        float raio = 1.0f;

        for (int i = 0; i < quantidade; i++) //randomizador de posi��o
        {
            Vector2 deslocamento = new Vector2(
                Random.Range(-raio, raio),
                Random.Range(-raio, raio)
            );
            Vector3 posicaoDrop = transform.position + (Vector3)deslocamento;

            Instantiate(objetoADrop, posicaoDrop, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Melee"))
    //    {
    //        ReceberDano(1);
     //   }
        
    //}
}

//OBS: o min�rio precisa ter 2 box collider, uma para a hitbox do minerador (essa sendo trigger), e outra para a hitbox do min�rio.