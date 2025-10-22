using UnityEngine;

public class E : MonoBehaviour
{
    public GameObject e;

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player") && !e.activeSelf)
        {
            e.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player") && e.activeSelf)
        {
            e.SetActive(false);
        }
    }
}
