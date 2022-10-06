using System.Collections.Generic;
using UnityEngine;

public class MarketScreenView : ScreenView
{
    [SerializeField]
    private RectTransform marketItemSpawnPoint;
    [SerializeField]
    private MarketItemController marketItemPrefab;

    public void Initialize(List<ItemRuntime> marketItems)
    {
        foreach (var item in marketItems)
        {
            var spawnedItem = Instantiate(marketItemPrefab, marketItemSpawnPoint);
            var itemCache = item;
            spawnedItem.Initialize(
                item,
                () =>
                {
                    //DisplayPreview(itemCache);
                });
        }
    }
}
