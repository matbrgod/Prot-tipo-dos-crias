using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {       
        
         Destroy(gameObject);
       
    }
}
