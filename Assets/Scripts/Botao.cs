using UnityEngine;

public class Botao : MonoBehaviour
{
    private Player player;
    public GameObject porta;

    //ao ENTRAR na área de trigger
    private void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        //if (objectThatEntered.tag == "Jogador")//nojinho :(
        if (objectThatEntered.CompareTag("Player"))//:)
        {
            player = objectThatEntered.GetComponent<Player>();
        }
    }
    //caso FIQUE na área de trigger
    private void OnTriggerStay2D(Collider2D objectThatStayed)
    {
        if (objectThatStayed.CompareTag("Player") && player.interact)
        {   
            if (porta != null)
            {
                porta.SetActive(true);
            }

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
