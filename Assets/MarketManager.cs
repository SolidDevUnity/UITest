using System;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField]
    private DataManager dataManager;

    [Space]
    [SerializeField]
    private MarketScreenView marketScreenView;
    [SerializeField]
    private ItemPreviewScreenView itemPreviewScreenView;
    [SerializeField]
    private StartScreenView startScreenView;

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

            var gold = dataManager.GetPlayerGold();
            startScreenView.UpdateGoldText(gold);
        }
    }

    #region Toggle Screens
    public void ToggleMarketScreen(bool isActive)
    {
        marketScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            var items = dataManager.GetMarketItems();
            marketScreenView.Initialize(items, this);
        }
    }

    public void ToggleItemPreviewScreen(bool isActive)
    {
        itemPreviewScreenView.ToggleScreen(isActive);
    }

    public void ToggleItemPreviewScreen(bool isActive, ItemRuntime itemRuntime)
    {
        itemPreviewScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            itemPreviewScreenView.Initialize(itemRuntime);
        }
    }
    #endregion
}
