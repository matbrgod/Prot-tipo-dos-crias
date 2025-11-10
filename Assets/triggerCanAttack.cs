using UnityEngine;

public class triggerCanAttack : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.canAttack = false;
            Destroy(gameObject, 0.1f);
        }
    }
}
