using System.Collections.Generic;

public class MarketScreenView : ScreenListView
{
    public void Initialize(List<ItemRuntime> marketItems, MarketManager marketManager, DataManager dataManager)
    {
        RefreshItemList(
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
                    },
                    () =>
                    {
                        marketManager.ToggleItemPreviewScreen(true, item);
                    });
            });
    }
}
