using UnityEngine;

public class Build : MonoBehaviour
{
    // Arraste o prefab do objeto para este campo no Inspector
    public GameObject objetoParaColocar;

    void Update()
    {
        // Verifica se a tecla X foi pressionada
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Pega a posi��o do mouse na tela
            Vector3 mousePos = Input.mousePosition;

            // Define a dist�ncia da c�mera para o plano onde o objeto ser� instanciado
            mousePos.z = 10f; // Ajuste conforme necess�rio para o seu cen�rio

            // Converte a posi��o do mouse para coordenadas do mundo
            Vector3 posicaoMundo = Camera.main.ScreenToWorldPoint(mousePos);

            // Instancia o objeto na posi��o do mouse, sem rota��o
            Instantiate(objetoParaColocar, posicaoMundo, Quaternion.identity);
        }
    }
}
