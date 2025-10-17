using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.collider.CompareTag("Player"))
        {
           return;
        }
        else
        {
         Destroy(gameObject);
        }
       
    }
}
