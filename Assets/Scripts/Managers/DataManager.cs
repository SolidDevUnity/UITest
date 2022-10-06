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

    public List<ItemRuntime> GetMarketItems()
    {
        var marketItems = GetSavedData().marketItems;
        return GetRuntimeItems(marketItems);
    }

    public List<ItemRuntime> GetInventoryItems()
    {
        var inventoryItems = GetSavedData().inventoryItems;
        return GetRuntimeItems(inventoryItems);
    }

    private List<ItemRuntime> GetRuntimeItems(List<string> itemList)
    {
        var runtimeItems = new List<ItemRuntime>();

        // find matching items on allItems
        foreach (var item in itemList)
        {
            foreach (var gameItem in allItems.items)
            {
                if (gameItem.name.Equals(item))
                {
                    var spawnedRuntimeItem = new ItemRuntime(gameItem);
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
            savedData.inventoryItems = new List<string>();
            foreach (var item in starterItems.items)
            {
                savedData.inventoryItems.Add(item.name);
            }

            savedData.marketItems = new List<string>();
            foreach (var item in marketStarterItems.items)
            {
                savedData.marketItems.Add(item.name);
            }

            string newSaveDataJSON = JsonUtility.ToJson(savedData);
            PlayerPrefs.SetString(savedDataPrefKey, newSaveDataJSON);
        }

        return savedData;
    }
}
