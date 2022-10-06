using System.Collections.Generic;
using UnityEngine;

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

        SetSavedData(saveData);
    }

    public void DeductGold(int goldToDeduct)
    {
        var saveData = GetSavedData();
        saveData.playerGold -= goldToDeduct;

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

        var newMarketList = saveData.marketItems;
        newMarketList.Remove(itemDataStruct);
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

        var newInventoryitems = saveData.inventoryItems;
        newInventoryitems.Remove(itemDataStruct);
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
                    var spawnedRuntimeItem = new ItemRuntime(gameItem);
                    spawnedRuntimeItem.marketPrice = item.marketPrice;

                    runtimeItems.Add(spawnedRuntimeItem);
                }
            }
        }

        return runtimeItems;
    }

    private SaveData GetSavedData()
    {
        var savedData = new SaveData();

        if (PlayerPrefs.HasKey(savedDataPrefKey))
        {
            string savedDataJSON = PlayerPrefs.GetString(savedDataPrefKey);
            savedData = JsonUtility.FromJson<SaveData>(savedDataJSON);
        }
        else
        {
            // Create new data
            savedData.inventoryItems = new List<ItemDataStruct>();
            foreach (var item in starterItems.items)
            {
                var temp = new ItemDataStruct()
                {
                    itemName = item.name,
                    marketPrice = item.initialMarketPrice
                };
                savedData.inventoryItems.Add(temp);
            }

            savedData.marketItems = new List<ItemDataStruct>();
            foreach (var item in marketStarterItems.items)
            {
                var temp = new ItemDataStruct()
                {
                    itemName = item.name,
                    marketPrice = item.initialMarketPrice
                };
                savedData.marketItems.Add(temp);
            }

            savedData.playerGold = startingGold;

            SetSavedData(savedData);
        }

        return savedData;
    }
}
