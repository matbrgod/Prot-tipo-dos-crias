using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] ItemPrefabs;

    public string inventoryPanelObjectName = "InventoryPanel";

    private static InventoryController instance;
    public static InventoryController Instance => instance; // exposto para outros scripts

    private bool slotsInitialized = false;
    private List<InventorySaveData> cachedInventory = new List<InventorySaveData>();

    public bool IsReady { get; private set; } = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        itemDictionary = FindObjectOfType<ItemDictionary>();
        if (inventoryPanel == null)
        {
            var found = GameObject.Find(inventoryPanelObjectName);
            if (found != null) inventoryPanel = found;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        TryInitializeSlots();
    }

    private void OnDestroy()
    {
        if (instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();

        if (inventoryPanel == null || inventoryPanel.scene != scene)
        {
            var found = GameObject.Find(inventoryPanelObjectName);
            if (found != null)
                inventoryPanel = found;
            else
            {
                var slotInScene = FindObjectOfType<Slot>();
                if (slotInScene != null)
                    inventoryPanel = slotInScene.transform.parent.gameObject;
            }

            slotsInitialized = false;
            IsReady = false;
        }

        TryInitializeSlots();
    }

    private void TryInitializeSlots()
    {
        if (slotsInitialized) return;
        if (inventoryPanel == null)
        {
            Debug.LogWarning("InventoryController: inventoryPanel não encontrado para inicializar slots.");
            return;
        }

        if (cachedInventory != null && cachedInventory.Count > 0)
            SetInventoryItems(cachedInventory);
        else
            SetInventoryItems(null);

        slotsInitialized = true;
        IsReady = true;
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        if (inventoryPanel == null)
        {
            Debug.LogWarning("GetInventoryItems: inventoryPanel é null.");
            return invData;
        }

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null)
                    invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }

        cachedInventory = new List<InventorySaveData>(invData);
        Debug.Log($"GetInventoryItems: retornando {invData.Count} itens.");
        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        if (inventoryPanel == null)
        {
            Debug.LogWarning("InventoryController: inventoryPanel não atribuído ao SetInventoryItems.");
            return;
        }

        foreach (Transform child in inventoryPanel.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < slotCount; i++)
            Instantiate(slotPrefab, inventoryPanel.transform);

        if (inventorySaveData != null)
        {
            itemDictionary = itemDictionary ?? FindObjectOfType<ItemDictionary>();

            foreach (InventorySaveData data in inventorySaveData)
            {
                if (data.slotIndex < slotCount)
                {
                    Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                    GameObject itemPrefab = itemDictionary != null ? itemDictionary.GetItemPrefab(data.itemID) : null;
                    if (itemPrefab != null && slot != null)
                    {
                        GameObject item = Instantiate(itemPrefab, slot.transform);
                        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        slot.currentItem = item;
                    }
                    else if (itemPrefab == null)
                    {
                        Debug.LogWarning($"InventoryController: prefab para itemID {data.itemID} não encontrado no ItemDictionary.");
                    }
                }
            }

            cachedInventory = new List<InventorySaveData>(inventorySaveData);
        }
        else
        {
            cachedInventory = new List<InventorySaveData>();
        }

        IsReady = true;
        slotsInitialized = true;
        Debug.Log($"SetInventoryItems: aplicados {cachedInventory.Count} itens ao painel.");
    }

    public bool AddItem(GameObject itemPrefab)
    {
        Item itemToAdd = itemPrefab.GetComponent<Item>();
        if (itemToAdd == null) return false;
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item slotItem = slot.currentItem.GetComponent<Item>();
                if (slotItem != null && slotItem.ID == itemToAdd.ID)
                {
                    slotItem.AddToStack();
                    cachedInventory = GetInventoryItems();
                    Debug.Log($"AddItem: empilhou item ID {itemToAdd.ID}. Total cache {cachedInventory.Count}");
                    return true;
                }
            }
        }

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                cachedInventory = GetInventoryItems();
                Debug.Log($"AddItem: adicionou novo item ID {itemToAdd.ID}. Total cache {cachedInventory.Count}");
                return true;
            }
        }

        Debug.Log("Inventory Full");
        return false;
    }
}

