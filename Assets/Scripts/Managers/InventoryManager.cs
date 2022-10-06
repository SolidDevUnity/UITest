using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private DataManager dataManager;

    [Space]
    [SerializeField]
    private InventoryScreenView inventoryScreenView;
    [SerializeField]
    private ItemPreviewScreenView itemPreviewScreenView;

    public void ToggleInventoryScreen(bool isActive)
    {
        inventoryScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            var items = dataManager.GetInventoryItems();
            inventoryScreenView.Initialize(items);
        }
    }

    public void ToggleItemPreviewScreen(bool isActive)
    {
        itemPreviewScreenView.ToggleScreen(isActive);
        itemPreviewScreenView.Initialize(inventoryScreenView.displayedItem);
    }
}
