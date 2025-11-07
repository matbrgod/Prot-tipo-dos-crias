using UnityEngine;

public class Botao : MonoBehaviour
{
    private Player player;
    private bool canPress;
    [SerializeField] private GameObject triggerE;
    public GameObject porta;

    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E))
        {
            if (porta != null)
            {
                porta.SetActive(true);
            }

            triggerE.SetActive(false);
            Destroy(gameObject, 0.15f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPress = true;

        }
    }
}