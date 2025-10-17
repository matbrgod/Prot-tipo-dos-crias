using UnityEngine;

public class spaceBar_trigger : MonoBehaviour
{
   public GameObject spaceBar;

    private void OnTriggerStay2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            spaceBar.SetActive(true);
        }
    }
}
