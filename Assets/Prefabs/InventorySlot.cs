using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountText;
    public Image background;

    private ItemData currentItem;

    public void SetItem(ItemData item)
    {
        Debug.Log("Setting item: " + item.itemName);
        currentItem = item;
        icon.sprite = item.icon;
        icon.color = Color.white; // tamamen görünür yap
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void SetEmpty()
    {
        currentItem = null;
        icon.sprite = null;

    }
}