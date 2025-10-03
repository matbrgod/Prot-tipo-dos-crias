using UnityEngine;

public class ácido : MonoBehaviour
{
    public float healthEnemy;
    public float maxHealthEnemy = 1f;
    public float velocidade = 3f;
    public float tempoDeVida = 2f;
    public GameObject prefabAoDestruir;
    public int danoAoPlayer = 1;

    private Transform jogador;
    private float tempoDecorrido = 0f;

    private enum Estado
    {
        Perseguir,
        Explodir,
        ColidiuComPlayer
    }

    private Estado estadoAtual = Estado.Perseguir;

    void Start()
    {
        GameObject objJogador = GameObject.FindGameObjectWithTag("Player");
        if (objJogador != null)
            jogador = objJogador.transform;
    }

    void Update()
    {
        switch (estadoAtual)
        {
            case Estado.Perseguir:
                if (jogador != null)
                {
                    Vector2 direcao = (jogador.position - transform.position).normalized;
                    transform.position += (Vector3)direcao * velocidade * Time.deltaTime;
                }

                tempoDecorrido += Time.deltaTime;
                if (tempoDecorrido >= tempoDeVida)
                {
                    estadoAtual = Estado.Explodir;
                }
                break;

            case Estado.ColidiuComPlayer:
                // Ao colidir, apenas muda para Explodir no próximo frame
                estadoAtual = Estado.Explodir;
                break;

            case Estado.Explodir:
                if (prefabAoDestruir != null)
                    Instantiate(prefabAoDestruir, transform.position, Quaternion.identity);

                Destroy(gameObject);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (estadoAtual != Estado.Perseguir)
            return;

        if (collision.CompareTag("Player"))
        {
            // Tenta chamar o método de dano no player
            var playerScript = collision.GetComponent<MonoBehaviour>();
            if (playerScript != null)
            {
                var metodo = playerScript.GetType().GetMethod("TomarDano");
                if (metodo != null)
                {
                    metodo.Invoke(playerScript, new object[] { danoAoPlayer });
                }
            }
            estadoAtual = Estado.ColidiuComPlayer;
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