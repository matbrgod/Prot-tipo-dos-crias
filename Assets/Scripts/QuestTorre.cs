using UnityEngine;

public class QuestTorre : MonoBehaviour
{
    public Item item;
    public Player player;
    public Slot slot;
    public bool canCraft;
    public int idProcurado = 1;
    [SerializeField] private GameObject torre;
    [SerializeField] private GameObject inventoryPage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();
        // Encontra o objeto Inventory page na cena
        if (inventoryPage == null)
        {
            Debug.LogError("Inventory page não encontrado na cena.");
            return;
        }

        // Busca todos os slots filhos do Inventory page
        Slot[] slots = inventoryPage.GetComponentsInChildren<Slot>(true);

        foreach (Slot s in slots)
        {
            Item itemNoSlot = s.GetComponentInChildren<Item>(true);
            if (itemNoSlot != null && itemNoSlot.ID == idProcurado)
            {
                slot = s;
                item = itemNoSlot;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (item.quantity >= 4 && canCraft == true && player.interact)
        {
            torre.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canCraft = true;
        }
    }
}
