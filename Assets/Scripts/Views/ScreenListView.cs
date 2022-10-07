using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenListView : ScreenView
{
    [SerializeField]
    protected RectTransform itemSpawnPoint;
    [SerializeField]
    protected ItemController itemPrefab;

    protected List<int> spawnedItemsID = new List<int>();

    protected virtual void RefreshItemList(List<ItemRuntime> items, Action<ItemRuntime, RectTransform, ItemController> OnSpawnItemAction)
    {
        // spawn missing items
        var itemsToSpawn = items.Where(i => !spawnedItemsID.Any(siid => siid == i.itemDataStruct.itemID)).ToList();
        foreach (var item in itemsToSpawn)
        {
            OnSpawnItemAction?.Invoke(item, itemSpawnPoint, itemPrefab);
            spawnedItemsID.Add(item.itemDataStruct.itemID);
        }

        // remove excess items
        var itemsToDestroyID = spawnedItemsID.Where(siid => !items.Any(i => i.itemDataStruct.itemID == siid)).ToList();
        foreach (Transform item in itemSpawnPoint)
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
