using UnityEngine;

public class Placa_Tutorial : MonoBehaviour
{
    public GameObject placa;

    private void OnTriggerStay2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            placa.SetActive(true);
            //Object.Destroy(this.gameObject);
        }
    }
}
