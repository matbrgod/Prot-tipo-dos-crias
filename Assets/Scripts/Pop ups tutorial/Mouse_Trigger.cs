using UnityEngine;

public class Mouse_Trigger : MonoBehaviour
{
    public GameObject mouse;

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player") && !mouse.activeSelf)
        {
            mouse.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player") && mouse.activeSelf)
        {
            mouse.SetActive(false);
        }
    }
}
