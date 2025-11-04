using UnityEngine;

public class wasd_trigger : MonoBehaviour
{
    public GameObject wasd;

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            wasd.SetActive(false);
        }
    }
}
    
