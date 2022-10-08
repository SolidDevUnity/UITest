using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Saves JSON on PlayerPrefs for testing purposes
public class DataManager : MonoBehaviour
{
    private const string savedDataPrefKey = "SavedItems";

    [SerializeField]
    private ItemSOHolder allItems;
    [SerializeField]
    private ItemSOHolder starterItems;
    [SerializeField]
    private ItemSOHolder marketStarterItems;
    [SerializeField]
    private int startingGold = 1000;

    public Action<int> OnGoldAmountUpdate;

    #region Gold
    public int GetPlayerGold()
    {
        var goldAmount = GetSavedData().playerGold;
        return goldAmount;
    }

    public void AddGold(int goldToAdd)
    {
        var saveData = GetSavedData();
        saveData.playerGold += goldToAdd;
        OnGoldAmountUpdate?.Invoke(saveData.playerGold);

        SetSavedData(saveData);
    }

    public void DeductGold(int goldToDeduct)
    {
        var saveData = GetSavedData();
        saveData.playerGold -= goldToDeduct;
        OnGoldAmountUpdate?.Invoke(saveData.playerGold);

        SetSavedData(saveData);
    }
    #endregion

    #region Market
    public List<ItemRuntime> GetMarketItems()
    {
        var marketItems = GetSavedData().marketItems;
        return GetRuntimeItems(marketItems);
    }

    public void AddMarketItem(ItemDataStruct itemDataStruct)
    {
        var saveData = GetSavedData();

        var newMarketList = saveData.marketItems;
        newMarketList.Add(itemDataStruct);
        saveData.marketItems = newMarketList;

        SetSavedData(saveData);
    }

    public void RemoveMarketItem(ItemDataStruct itemDataStruct)
    {
        var saveData = GetSavedData();

        var itemToRemove = saveData.marketItems.First(i => i.itemID == itemDataStruct.itemID);

        var newMarketList = saveData.marketItems;
        newMarketList.Remove(itemToRemove);
        saveData.marketItems = newMarketList;

        SetSavedData(saveData);
    }
    #endregion

    #region Inventory
    public List<ItemRuntime> GetInventoryItems()
    {
        var inventoryItems = GetSavedData().inventoryItems;
        return GetRuntimeItems(inventoryItems);
    }

    public void AddInventoryItem(ItemDataStruct itemDataStruct)
    {
        var saveData = GetSavedData();

        var newInventoryitems = saveData.inventoryItems;
        newInventoryitems.Add(itemDataStruct);
        saveData.inventoryItems = newInventoryitems;

        SetSavedData(saveData);
    }

    public void RemoveInventoryItem(ItemDataStruct itemDataStruct)
    {
        var saveData = GetSavedData();

        var itemToRemove = saveData.inventoryItems.First(i => i.itemID == itemDataStruct.itemID);

        var newInventoryitems = saveData.inventoryItems;
        newInventoryitems.Remove(itemToRemove);
        saveData.inventoryItems = newInventoryitems;

        SetSavedData(saveData);
    }
    #endregion

    private void SetSavedData(SaveData newSaveData)
    {
        string newSaveDataJSON = JsonUtility.ToJson(newSaveData);
        PlayerPrefs.SetString(savedDataPrefKey, newSaveDataJSON);
    }

    private List<ItemRuntime> GetRuntimeItems(List<ItemDataStruct> itemList)
    {
        var runtimeItems = new List<ItemRuntime>();

        // find matching items on allItems
        foreach (var item in itemList)
        {
            foreach (var gameItem in allItems.items)
            {
                if (gameItem.name.Equals(item.itemName))
                {
                    var spawnedRuntimeItem = new ItemRuntime(gameItem, item);
                    runtimeItems.Add(spawnedRuntimeItem);
                }
            }
        }

        return runtimeItems;
    }

    private SaveData GetSavedData()
    {
        var saveData = new SaveData();

        if (PlayerPrefs.HasKey(savedDataPrefKey))
        {
            string savedDataJSON = PlayerPrefs.GetString(savedDataPrefKey);
            saveData = JsonUtility.FromJson<SaveData>(savedDataJSON);
        }
        else
        {
            // Create new data
            saveData.inventoryItems = new List<ItemDataStruct>();
            foreach (var item in starterItems.items)
            {
                var temp = new ItemDataStruct()
                {
                    itemID = GenerateUniqueItemID(item, saveData.inventoryItems.Count),
                    itemName = item.name
                };
                saveData.inventoryItems.Add(temp);
            }

            saveData.marketItems = new List<ItemDataStruct>();
            foreach (var item in marketStarterItems.items)
            {
                var temp = GenerateItemDataStruct(item, saveData.marketItems.Count);
                saveData.marketItems.Add(temp);
            }

            saveData.playerGold = startingGold;
            OnGoldAmountUpdate?.Invoke(saveData.playerGold);

            SetSavedData(saveData);
        }

        return saveData;
    }

    public ItemDataStruct GenerateItemDataStruct(ItemSO item, int itemIndex)
    {
        var temp = new ItemDataStruct()
        {
            itemID = GenerateUniqueItemID(item, itemIndex),
            itemName = item.name,
            marketPrice = UnityEngine.Random.Range(item.priceRange.minPrice, item.priceRange.maxPrice)
        };

        return temp;
    }

    private int GenerateUniqueItemID(ItemSO itemSO, int itemIndex)
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int hashCode = itemSO.GetHashCode();
        int randomNumber = UnityEngine.Random.Range(0, 9999);

        // probably a good enough unique id
        int uniqueID =
            hashCode + // avoids same id for different items
            cur_time + // avoids same id for same items
            itemIndex + // avoids same id for same items generated at the same list
            randomNumber; // minimizes chance of same id of same items generated at the same time

        return uniqueID;
    }
}
