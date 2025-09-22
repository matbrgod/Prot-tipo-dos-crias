using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // UI icon of the item
    public DraggableItem item; // Now it uses DraggableItem

    // Add item to the slot
    public void AddItem(DraggableItem newItem)
    {
        item = newItem;
        icon.sprite = item.GetComponent<Image>().sprite;
        icon.enabled = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    // Check if slot is empty
    public bool IsEmpty()
    {
        return item == null;
    }
}
