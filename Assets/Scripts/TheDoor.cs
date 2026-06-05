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

    void CheckInventory()
    {
        for (int i = 0; i < Inventory.instance.altBar.Length; i++)
        {
            // null kontrolü þart
            if (Inventory.instance.altBar[i] == null) continue;

            if (Inventory.instance.altBar[i] == searchedItem)
            {
                Debug.Log("Kapý açýldý!");
                Destroy(gameObject);
                return;
            }
        }
        Debug.Log("Anahtarýn yok!");
    }
}