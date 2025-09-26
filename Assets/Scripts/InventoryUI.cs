using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject fullInventory; // this will be Toolbar (1)

    public void ToggleInventory()
    {
        bool isActive = fullInventory.activeSelf;
        fullInventory.SetActive(!isActive); // turn ON if OFF, OFF if ON
    }
}
