using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PegarArma : MonoBehaviour
{
    public GameObject arma;
    public GameObject triggerE;
    public GameObject quest0;
    public GameObject quest1;
    public GameObject armaUI;
    private bool canPress;
    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E))
        {
            if (arma != null)
            {
                arma.SetActive(true);
            }
            var player = FindObjectOfType<Player>();
            if (player != null) player.canAttack = true;
            quest0.SetActive(false);
            quest1.SetActive(true);
            armaUI.SetActive(true);
            triggerE.SetActive(false);
            Destroy(gameObject, 0.15f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPress = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPress = false;
            

        }
    }
}
