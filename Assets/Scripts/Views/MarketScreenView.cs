using System.Collections.Generic;

public class MarketScreenView : ScreenListView
{
    public void Initialize(List<ItemRuntime> marketItems, MarketManager marketManager)
    {
        RefreshItemList(
            marketItems,
            (item, itemSpawnPoint, itemPrefab) =>
            {
                var spawnedItem = Instantiate(itemPrefab, itemSpawnPoint) as MarketItemController;
                spawnedItem.Initialize(
                    item,
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
