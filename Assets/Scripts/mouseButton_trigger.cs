using UnityEngine;
using System.Collections;

public class mouseButton_trigger : MonoBehaviour
{
    public GameObject mouseButton;
    private Coroutine disableCoroutine; 

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            mouseButton.SetActive(true);
            if (disableCoroutine != null) StopCoroutine(disableCoroutine);
            disableCoroutine = StartCoroutine(DisableAfterDelay(5f));
        }
    }

    private IEnumerator DisableAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        mouseButton.SetActive(false);
        disableCoroutine = null;
    }
}