using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerCollectItem : MonoBehaviour
{
    private InventoryController InventoryController;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryController = FindObjectOfType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                bool itemAdded = InventoryController.AddItem(collision.gameObject);
                if (itemAdded)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
