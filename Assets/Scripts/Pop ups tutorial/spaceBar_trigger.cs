using UnityEngine;

public class spaceBar_trigger : MonoBehaviour
{
   public GameObject spaceBar;

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            spaceBar.SetActive(!spaceBar.activeSelf);
            Object.Destroy(this.gameObject);
        }
    }
}
