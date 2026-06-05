using UnityEngine;

public class InventoryManager: MonoBehaviour
{    public static InventoryManager instance;

    void Awake()
    {
        instance = this;
    }
    public bool inIsOpen = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        inIsOpen = !inIsOpen;
        if (inIsOpen)
        {
            GameManager.instance.inventoryCanvas.SetActive(true);
             GameManager.instance.playerMovement.enabled = false;
            Time.timeScale = 0f; 
        }
        else
        {
            GameManager.instance.inventoryCanvas.SetActive(false);
            GameManager.instance.playerMovement.enabled = true;
            Time.timeScale = 1f;


        }
    }

}
