using UnityEngine;

public class TheKey: MonoBehaviour
{
    public ItemData keyData; // Anahtarýn item verisi
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        Inventory.instance.AddItem(keyData);
            Destroy(gameObject);
        }
    }
}
