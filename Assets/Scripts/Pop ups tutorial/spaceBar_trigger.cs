using UnityEngine;

public class spaceBar_trigger : MonoBehaviour
{
   public GameObject spaceBar;

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            if(spaceBar != null) 
                spaceBar.SetActive(false);

            Object.Destroy(this.gameObject);
        }
    }
}
