using UnityEngine;

public class mouseButton_TriggerDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject mouseTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mouseTrigger.SetActive(false);
        }
    }
}
