using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketScreenView : ScreenListView
{
    [SerializeField]
    private TextMeshProUGUI goldAmountText;

    public void Initialize(List<ItemRuntime> marketItems, MarketManager marketManager, DataManager dataManager)
    {
        UpdateGoldText(dataManager);

        base.RefreshItemList(
            marketItems,
            (item, itemSpawnPoint, itemPrefab) =>
            {
                var spawnedItem = Instantiate(itemPrefab, itemSpawnPoint) as MarketItemController;
                spawnedItem.Initialize(
                    item,
                    dataManager,
                    () =>
                    {
                        marketManager.BuyItem(item.itemDataStruct);
                        UpdateGoldText(dataManager);
                    },
                    () =>
                    {
                        marketManager.ToggleItemPreviewScreen(true, item);
                    });
            });
    }

    private void UpdateGoldText(DataManager dataManager)
    {
        int playerGold = dataManager.GetPlayerGold();
        goldAmountText.text = "Player Gold: " + playerGold.ToString();
    }
}
