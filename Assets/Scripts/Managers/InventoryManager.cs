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
    [SerializeField]
    private PutUpForSaleScreenView putUpForSaleScreenView;
    [SerializeField]
    private DeleteConfirmationScreenView deleteConfirmationScreenView;

    public void DeleteItemFromInventory(ItemDataStruct item)
    {
        ToggleItemPreviewScreen(false);
        dataManager.RemoveInventoryItem(item);
        ToggleDeleteConfirmationScreen(false);
        ToggleInventoryScreen(true);
    }

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

    public void TogglePutUpForSaleScreen(bool isActive)
    {
        putUpForSaleScreenView.ToggleScreen(isActive);
    }

    public void ToggleDeleteConfirmationScreen(bool isActive)
    {
        deleteConfirmationScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            deleteConfirmationScreenView.Initialize(inventoryScreenView.displayedItem, this);
        }
    }
}
