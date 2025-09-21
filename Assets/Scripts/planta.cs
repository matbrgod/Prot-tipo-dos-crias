using UnityEngine;

public class planta : MonoBehaviour
{
    public float distanciaParaAtacar = 5f;
    public float tempoEntreTiros = 2f;
    public GameObject projetilPrefab;
    public Transform pontoDeDisparo;

    private Transform jogador;
    private float tempoUltimoTiro;
    private Estado estadoAtual = Estado.Idle;

    private enum Estado
    {
        Idle,
        Atacando
    }

    void Start()
    {
        GameObject objJogador = GameObject.FindGameObjectWithTag("Player");
        if (objJogador != null)
            jogador = objJogador.transform;
    }

    void Update()
    {
        if (jogador == null)
            return;

        float distancia = Vector2.Distance(transform.position, jogador.position);

        switch (estadoAtual)
        {
            case Estado.Idle:
                if (distancia <= distanciaParaAtacar)
                    estadoAtual = Estado.Atacando;
                break;

            case Estado.Atacando:
                if (distancia > distanciaParaAtacar)
                {
                    estadoAtual = Estado.Idle;
                }
                else
                {
                    Atacar();
                }
                break;
        }
    }

    void Atacar()
    {
        if (Time.time - tempoUltimoTiro >= tempoEntreTiros)
        {
            Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.identity);
            tempoUltimoTiro = Time.time;
        }
    }
}