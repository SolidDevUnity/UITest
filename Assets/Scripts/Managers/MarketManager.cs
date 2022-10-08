using UnityEngine;

public class MarketManager : ScreenManager
{
    [Space]
    [SerializeField]
    private MarketScreenView marketScreenView;
    [SerializeField]
    private ItemPreviewScreenView itemPreviewScreenView;

    public void BuyItem(ItemDataStruct itemDataStruct)
    {
        var price = itemDataStruct.marketPrice;
        var playerGold = dataManager.GetPlayerGold();

        bool hasEnoughGold = playerGold >= price;
        if (hasEnoughGold)
        {
            dataManager.DeductGold(price);
            dataManager.RemoveMarketItem(itemDataStruct);
            dataManager.AddInventoryItem(itemDataStruct);
            ToggleMarketScreen(true);
        }
    }

    #region Toggle Screens
    public void ToggleMarketScreen(bool isActive)
    {
        ToggleScreen(
            isActive,
            marketScreenView,
            () =>
            {
                var items = dataManager.GetMarketItems();
                marketScreenView.Initialize(items, this, dataManager);
            });
    }

    public void ToggleItemPreviewScreen(bool isActive)
    {
        ToggleScreen(
            isActive,
            itemPreviewScreenView);
    }

    public void ToggleItemPreviewScreen(bool isActive, ItemRuntime itemRuntime)
    {
        ToggleScreen(
            isActive,
            itemPreviewScreenView,
            () =>
            {
                itemPreviewScreenView.Initialize(itemRuntime);
            });
    }
    #endregion
}
