using UnityEngine;

public class Collectable : MonoBehaviour
{
    private PlayerMovement player;

    //ao ENTRAR na área de trigger
    private void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        //if (objectThatEntered.tag == "Jogador")//nojinho :(
        if (objectThatEntered.CompareTag("Player"))//:)
        {
            player = objectThatEntered.GetComponent<PlayerMovement>();
        }
    }
    //caso FIQUE na área de trigger
    private void OnTriggerStay2D(Collider2D objectThatStayed)
    {
        if (objectThatStayed.CompareTag("Player") && player.interact)
        {
            //realiza a ação do coletável
            //deleta ele da cena
            //Debug.Log("Player pegou o coletavel");
            Destroy(gameObject, 0.15f);
        }
    }
    //ao SAIR da área de trigger
    private void OnTriggerExit2D(Collider2D objectThatExit)
    {
        if (objectThatExit.CompareTag("Player"))
        {
            //Debug.Log("Jogador saiu da área");
            player = null;
        }
    }
}