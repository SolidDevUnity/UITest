using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private DataManager dataManager;
    [SerializeField]
    private InventoryScreenView inventoryScreenView;

    private void Start()
    {
        var items = dataManager.GetInventoryItems();
        inventoryScreenView.Initialize(items);
    }

    public void ToggleInventoryScreen(bool isActive)
    {
        inventoryScreenView.ToggleScreen(isActive);
    }
}
