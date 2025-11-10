using UnityEngine;

public class cantAttack : MonoBehaviour
{
    public Player player;
    public GameObject triggerNave;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.canAttack = true;
            triggerNave.SetActive(true);
            //Destroy(gameObject, 0.1f);
        }
    }
}
