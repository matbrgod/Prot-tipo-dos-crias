using UnityEngine;

public class Placa_Tutorial_Off : MonoBehaviour
{
    public GameObject placa;

    private void OnTriggerStay2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player") && placa.activeSelf)
        {
            placa.SetActive(false);
            //Object.Destroy(this.gameObject);
        }
    }
}
