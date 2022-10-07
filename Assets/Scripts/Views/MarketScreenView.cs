using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketScreenView : ScreenView
{
    [SerializeField]
    private RectTransform marketItemSpawnPoint;
    [SerializeField]
    private MarketItemController marketItemPrefab;

    private List<int> spawnedItemsID = new List<int>();

    public void Initialize(List<ItemRuntime> marketItems, MarketManager marketManager)
    {
        RefreshItemList(marketItems, marketManager);
    }

    private void RefreshItemList(List<ItemRuntime> items, MarketManager marketManager)
    {
        // spawn missing items
        var itemsToSpawn = items.Where(i => !spawnedItemsID.Any(siid => siid == i.itemDataStruct.itemID)).ToList();
        foreach (var item in itemsToSpawn)
        {
            var spawnedItem = Instantiate(marketItemPrefab, marketItemSpawnPoint);
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

            spawnedItemsID.Add(item.itemDataStruct.itemID);
        }

        // remove excess items
        var itemsToDestroyID = spawnedItemsID.Where(siid => !items.Any(i => i.itemDataStruct.itemID == siid)).ToList();
        foreach (Transform item in marketItemSpawnPoint)
        {
            int alreadySpawnedItemID = item.GetComponent<ItemController>().itemRuntime.itemDataStruct.itemID;
            bool itemIsToBeDestroyed = itemsToDestroyID.Any(its => its == alreadySpawnedItemID);
            if (itemIsToBeDestroyed)
            {
                Destroy(item.gameObject);
                spawnedItemsID.Remove(alreadySpawnedItemID);
            }
        }
    }
}
