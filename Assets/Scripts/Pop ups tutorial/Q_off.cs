using UnityEngine;

public class Q_off : MonoBehaviour
{
    public GameObject spaceBar;
    public GameObject Q;

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            if(spaceBar != null) 
                spaceBar.SetActive(true);
            if(Q != null) 
                Q.SetActive(false);
            Object.Destroy(this.gameObject);
        }
    }
}
