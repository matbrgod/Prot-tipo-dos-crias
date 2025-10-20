using UnityEngine;
using System.Collections;

public class TriggerRatao : MonoBehaviour
{
    public GameObject vidaDoBoss;
    public GameObject animacao;
    
    public GameObject SpawnerRatinhos;

    private void OnTriggerStay2D(Collider2D objectThatStayed)
    {
        
        if (objectThatStayed.CompareTag("Player"))
        {
            if (animacao != null && !animacao.activeSelf)
            {
                animacao.SetActive(true);
                StartCoroutine(ActivateTriggerWithDelay());
            }
            SpawnerRatinhos.SetActive(true);
            
        }
    }

    private IEnumerator ActivateTriggerWithDelay()
    {
        yield return new WaitForSeconds(1f);
        if (vidaDoBoss != null)
        {
            vidaDoBoss.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        Object.Destroy(this.gameObject);
    }
}
