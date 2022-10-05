using System.Collections.Generic;
using UnityEngine;

// Saves JSON on PlayerPrefs for testing purposes
public class DataManager : MonoBehaviour
{
    private const string savedItemsPrefKey = "SavedItems";

    [SerializeField]
    private ItemSOHolder allItems;
    [SerializeField]
    private ItemSOHolder starterItems;

    public List<ItemRuntime> GetInventoryItems()
    {
        var savedItems = GetSavedItemsData();
        var runtimeItems = new List<ItemRuntime>();

        // find matching items on allItems
        foreach (var savedItem in savedItems)
        {
            foreach (var gameItem in allItems.items)
            {
                if (gameItem.name.Equals(savedItem))
                {
                    var spawnedRuntimeItem = new ItemRuntime(gameItem);
                    runtimeItems.Add(spawnedRuntimeItem);
                }
            }
        }

        return runtimeItems;
    }

    private List<string> GetSavedItemsData()
    {
        var savedData = new List<string>();

        if (PlayerPrefs.HasKey(savedItemsPrefKey))
        {
            string savedDataJSON = PlayerPrefs.GetString(savedItemsPrefKey);
            savedData = JsonUtility.FromJson<List<string>>(savedDataJSON);
        }
        else
        {
            // Create new data
            savedData = new List<string>();
            foreach (var item in starterItems.items)
            {
                savedData.Add(item.name);
            }

            string newSaveDataJSON = JsonUtility.ToJson(savedData);
            PlayerPrefs.SetString(savedItemsPrefKey, newSaveDataJSON);
        }

        return savedData;
    }
}
