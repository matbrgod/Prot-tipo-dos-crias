using UnityEngine;

public class QuestTorre : MonoBehaviour
{
    public Item item;
    public Player player;
    public Slot slot;
    public bool canCraft;
    public int idProcurado = 1;
    public Animator painelAnimator;
    [SerializeField] private GameObject torre;
    [SerializeField] private GameObject inventoryPage;
    [SerializeField] private GameObject quest3;
    [SerializeField] private GameObject quest4;
    [SerializeField] private GameObject quest5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();
        // Encontra o objeto Inventory page na cena
        if (inventoryPage == null)
        {
            Debug.LogError("Inventory page n�o encontrado na cena.");
            return;
        }

        // Busca todos os slots filhos do Inventory page
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcurarItemNoInventario();

        if (item.quantity >= 40)
        {
            if (quest3 != null)
                quest3.SetActive(false);

            if (quest4 != null)
                quest4.SetActive(true);
        }

        if (item.quantity >= 40 && canCraft == true && player.interact)
        {
            painelAnimator.SetBool("Ativar", true);

            if (quest4 != null)
                quest4.SetActive(false);

            item.quantity -= 40;
            //slot.UpdateSlotUI();
            torre.SetActive(true);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canCraft = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canCraft = false;
            painelAnimator.SetBool("Desativar", true);
            painelAnimator.SetBool("Ativar", false);

            if (quest5 != null)
                quest5.SetActive(true);
            
        }
    }
    void ProcurarItemNoInventario()
    {
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
}
