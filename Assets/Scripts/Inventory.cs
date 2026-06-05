using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public ItemData[] altBar = new ItemData[9];

    void Awake() { instance = this; }

    public void AddItem(ItemData item)
    {
        Debug.Log("Adding item: " + item.itemName);

        for (int i = 0; i < altBar.Length; i++)
        {
            Debug.Log("Checking slot " + i + ": " + (altBar[i] == null ? "Empty" : altBar[i].itemName));

            if (altBar[i] == null)
            {
                Debug.Log("In For" );

                altBar[i] = item;
                InventoryUI.instance.UpdateSlot(i, item);
                return;
            }
        }
    }
}