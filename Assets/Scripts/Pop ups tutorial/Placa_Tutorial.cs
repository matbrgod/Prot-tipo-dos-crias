using UnityEngine;

public class Placa_Tutorial : MonoBehaviour
{
    public GameObject placa;

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            placa.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            placa.SetActive(false);
        }
    }
}
