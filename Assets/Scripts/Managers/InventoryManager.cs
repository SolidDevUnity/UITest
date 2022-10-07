using System;
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
    [SerializeField]
    private StartScreenView startScreenView;

    private ItemRuntime displayedItem;

    public void PutUpItemForSale(ItemDataStruct item)
    {
        dataManager.RemoveInventoryItem(item);
        dataManager.AddGold(item.marketPrice);

        // add gold update event instead
        var gold = dataManager.GetPlayerGold();
        startScreenView.UpdateGoldText(gold);

        dataManager.AddMarketItem(item);

        ToggleItemPreviewScreen(false);
        TogglePutUpForSaleScreen(false);
        ToggleInventoryScreen(true);
    }

    public void DeleteItemFromInventory(ItemDataStruct item)
    {
        dataManager.RemoveInventoryItem(item);
        ToggleItemPreviewScreen(false);
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
        ToggleScreen(
            isActive,
            inventoryScreenView,
            () =>
            {
                var items = dataManager.GetInventoryItems();
                inventoryScreenView.Initialize(items, this);
            });
    }

    public void ToggleItemPreviewScreen(bool isActive)
    {
        ToggleScreen(
            isActive,
            itemPreviewScreenView,
            () =>
            {
                itemPreviewScreenView.Initialize(displayedItem);
            });
    }

    public void TogglePutUpForSaleScreen(bool isActive)
    {
        ToggleScreen(
            isActive,
            putUpForSaleScreenView,
            () =>
            {
                putUpForSaleScreenView.Initialize(displayedItem, this);
            });
    }

    public void ToggleDeleteConfirmationScreen(bool isActive)
    {
        ToggleScreen(
            isActive,
            deleteConfirmationScreenView,
            () =>
            {
                deleteConfirmationScreenView.Initialize(displayedItem, this);
            });
    }

    public void ToggleScreen(bool isActive, ScreenView screenView, Action onActiveAction = null, Action onInActiveAction = null)
    {
        screenView.ToggleScreen(isActive);

        if (isActive)
        {
            onActiveAction?.Invoke();
        }
        else
        {
            onInActiveAction?.Invoke();
        }
    }
    #endregion
}
