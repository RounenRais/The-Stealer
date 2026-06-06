using System;
using UnityEngine;

public class TheDoor : MonoBehaviour
{
    public ItemData searchedItem;
    private bool playerNearby = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            CheckInventory();
        }
    }
    void Open(int index,GameObject selectedObject) {
        InventoryUI.instance.UpdateSlot(index, null);
        Destroy(selectedObject);
    }
    void CheckInventory()
    {
        for (int i = 0; i < Inventory.instance.altBar.Length; i++)
        {
            if (Inventory.instance.altBar[i] == null) continue;

            if (Inventory.instance.altBar[i] == searchedItem)
            {
                // Anahtarý envanterden kaldýr
                Inventory.instance.altBar[i] = null;
                InventoryUI.instance.slots[i].SetEmpty();

                Debug.Log("Kapý açý ldý!");
                Destroy(gameObject);
                return;
            }
        }
        Debug.Log("Anahtarýn yok!");
    }
}