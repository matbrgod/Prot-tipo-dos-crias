using UnityEngine;

public class E_off : MonoBehaviour
{
    public GameObject e;

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player") && e != null && e.activeSelf)
        {
            e.SetActive(false);
        }
    }
}