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
    
    private ItemRuntime displayedItem;

    public void PutUpItemForSale(ItemDataStruct item)
    {
        ToggleItemPreviewScreen(false);
        TogglePutUpForSaleScreen(false);
        ToggleInventoryScreen(true);
        dataManager.RemoveInventoryItem(item);
        dataManager.AddMarketItem(item);
    }

    public void DeleteItemFromInventory(ItemDataStruct item)
    {
        ToggleItemPreviewScreen(false);
        dataManager.RemoveInventoryItem(item);
        ToggleDeleteConfirmationScreen(false);
        ToggleInventoryScreen(true);
    }

    public void SetDisplayedItem(ItemRuntime itemRuntime)
    {
        displayedItem = itemRuntime;
    }

    #region Toggle Screens
    public void ToggleInventoryScreen(bool isActive)
    {
        inventoryScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            var items = dataManager.GetInventoryItems();
            inventoryScreenView.Initialize(items, this);
        }
    }

    public void ToggleItemPreviewScreen(bool isActive)
    {
        itemPreviewScreenView.ToggleScreen(isActive);
        itemPreviewScreenView.Initialize(displayedItem);
    }

    public void TogglePutUpForSaleScreen(bool isActive)
    {
        putUpForSaleScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            putUpForSaleScreenView.Initialize(displayedItem, this);
        }
    }

    public void ToggleDeleteConfirmationScreen(bool isActive)
    {
        deleteConfirmationScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            deleteConfirmationScreenView.Initialize(displayedItem, this);
        }
    }
    #endregion
}
