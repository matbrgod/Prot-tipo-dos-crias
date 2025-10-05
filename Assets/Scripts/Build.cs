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
            // Pega a posição do mouse na tela
            Vector3 mousePos = Input.mousePosition;

            // Define a distância da câmera para o plano onde o objeto será instanciado
            mousePos.z = 10f; // Ajuste conforme necessário para o seu cenário

            // Converte a posição do mouse para coordenadas do mundo
            Vector3 posicaoMundo = Camera.main.ScreenToWorldPoint(mousePos);

            // Instancia o objeto na posição do mouse, sem rotação
            Instantiate(objetoParaColocar, posicaoMundo, Quaternion.identity);
        }
    }
}
