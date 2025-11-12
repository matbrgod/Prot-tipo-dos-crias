using UnityEngine;

public class cantAttack : MonoBehaviour
{
    public Player player;
    public GameObject triggerNave;
    [SerializeField] GameObject trigger;
    [SerializeField] GameObject paredeCaverna;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.canAttack = true;

            if (triggerNave != null)
                triggerNave.SetActive(true);

            if (trigger != null)
            {
                trigger.SetActive(false);
                Destroy(gameObject, 0.1f);
            }
            if (paredeCaverna != null)
                paredeCaverna.SetActive(false);
            
        }
    }
}
