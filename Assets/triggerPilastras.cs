using UnityEngine;

public class triggerPilastras : MonoBehaviour
{
    [SerializeField] private GameObject luzPilastra;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("On");
            if (luzPilastra != null)
            {
                luzPilastra.SetActive(true);
            }
        }
    }
}
