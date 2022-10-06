using System.Collections.Generic;
using UnityEngine;

public class MarketScreenView : ScreenView
{
    [SerializeField]
    private RectTransform marketItemSpawnPoint;
    [SerializeField]
    private MarketItemController marketItemPrefab;

    public void Initialize(List<ItemRuntime> marketItems, MarketManager marketManager)
    {
        foreach (Transform item in marketItemSpawnPoint)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in marketItems)
        {
            var spawnedItem = Instantiate(marketItemPrefab, marketItemSpawnPoint);
            var itemCache = item;
            var marketManagerCache = marketManager;
            spawnedItem.Initialize(
                itemCache,
                () =>
                {
                    var temp = new ItemDataStruct()
                    {
                        itemName = itemCache.itemName,
                        marketPrice = itemCache.marketPrice
                    };
                    marketManagerCache.BuyItem(temp);
                    marketManagerCache.ToggleMarketScreen(true);
                },
                () =>
                {
                    marketManagerCache.ToggleItemPreviewScreen(true, itemCache);
                });
        }
    }
}
