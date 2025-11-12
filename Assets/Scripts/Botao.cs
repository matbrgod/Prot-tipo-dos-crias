using UnityEngine;

public class Botao : MonoBehaviour
{
    private Player player;
    private bool canPress;
    [SerializeField] private GameObject triggerE;
    public GameObject porta;
    public GameObject porta2;

    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E))
        {
            if (porta != null)
            {
                porta.SetActive(true);
            }
            if (porta2 != null)
                porta2.SetActive(true);

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