using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public InventorySlot[] slots; // Inspector'dan 9 slotu sürükle

    void Awake() { instance = this; }

    public void UpdateSlot(int index, ItemData item)
    {
        Debug.Log($"Updating slot {index} with item: {item?.itemName ?? "null"}");
        slots[index].SetItem(item);
    }
}