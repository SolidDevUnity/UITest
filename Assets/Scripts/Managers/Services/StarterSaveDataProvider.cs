using System;
using System.Collections.Generic;
using UnityEngine;

public class StarterSaveDataProvider : MonoBehaviour, IInitialSaveDataProvider
{
    [SerializeField]
    private ItemSOHolder starterInventoryItems;
    [SerializeField]
    private ItemSOHolder starterMarketItems;
    [SerializeField]
    private int startingGold = 1000;

    public SaveData CreateInitialSaveData()
    {
        var saveData = new SaveData
        {
            inventoryItems = GenerateInventoryItems(),
            marketItems = GenerateMarketItems(),
            playerGold = startingGold
        };

        return saveData;
    }

    private List<ItemDataStruct> GenerateInventoryItems()
    {
        var result = new List<ItemDataStruct>();
        if (starterInventoryItems?.items == null)
        {
            return result;
        }

        for (int index = 0; index < starterInventoryItems.items.Length; index++)
        {
            var item = starterInventoryItems.items[index];
            if (item == null)
            {
                continue;
            }

            result.Add(new ItemDataStruct
            {
                itemID = GenerateUniqueItemID(item, index),
                itemName = item.name,
                marketPrice = 0
            });
        }

        return result;
    }

    private List<ItemDataStruct> GenerateMarketItems()
    {
        var result = new List<ItemDataStruct>();
        if (starterMarketItems?.items == null)
        {
            return result;
        }

        for (int index = 0; index < starterMarketItems.items.Length; index++)
        {
            var item = starterMarketItems.items[index];
            if (item == null)
            {
                continue;
            }

            result.Add(new ItemDataStruct
            {
                itemID = GenerateUniqueItemID(item, index),
                itemName = item.name,
                marketPrice = UnityEngine.Random.Range(item.priceRange.minPrice, item.priceRange.maxPrice)
            });
        }

        return result;
    }

    private int GenerateUniqueItemID(ItemSO itemSO, int itemIndex)
    {
        if (itemSO == null)
        {
            throw new ArgumentNullException(nameof(itemSO));
        }

        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int curTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int hashCode = itemSO.GetHashCode();
        int randomNumber = UnityEngine.Random.Range(0, 9999);

        return hashCode + curTime + itemIndex + randomNumber;
    }
}

