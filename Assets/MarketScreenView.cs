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
        foreach (Transform item in marketItemSpawnPoint)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in marketItems)
        {
            var spawnedItem = Instantiate(marketItemPrefab, marketItemSpawnPoint);
            var itemCache = item;
            spawnedItem.Initialize(
                item,
                () =>
                {
                    // Buy button
                    Debug.Log("buy");
                },
                () =>
                {
                    // About button
                    Debug.Log("about");
                });
        }
    }
}
