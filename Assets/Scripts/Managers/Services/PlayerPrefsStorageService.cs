using UnityEngine;

public class PlayerPrefsStorageService : MonoBehaviour, IDataStorageService
{
    [SerializeField]
    private string playerPrefsKey = "SavedItems";

    public bool HasSaveData()
    {
        return PlayerPrefs.HasKey(playerPrefsKey);
    }

    public SaveData Load()
    {
        if (!HasSaveData())
        {
            return CreateEmptySaveData();
        }

        var rawJson = PlayerPrefs.GetString(playerPrefsKey);
        if (string.IsNullOrEmpty(rawJson))
        {
            return CreateEmptySaveData();
        }

        var saveData = JsonUtility.FromJson<SaveData>(rawJson);
        if (saveData == null)
        {
            return CreateEmptySaveData();
        }

        EnsureCollections(saveData);
        return saveData;
    }

    public void Save(SaveData saveData)
    {
        if (saveData == null)
        {
            Debug.LogWarning("Attempted to save a null SaveData instance.");
            return;
        }

        EnsureCollections(saveData);
        var serialized = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(playerPrefsKey, serialized);
        PlayerPrefs.Save();
    }

    private static SaveData CreateEmptySaveData()
    {
        return new SaveData
        {
            inventoryItems = new System.Collections.Generic.List<ItemDataStruct>(),
            marketItems = new System.Collections.Generic.List<ItemDataStruct>(),
            playerGold = 0
        };
    }

    private static void EnsureCollections(SaveData saveData)
    {
        if (saveData.inventoryItems == null)
        {
            saveData.inventoryItems = new System.Collections.Generic.List<ItemDataStruct>();
        }

        if (saveData.marketItems == null)
        {
            saveData.marketItems = new System.Collections.Generic.List<ItemDataStruct>();
        }
    }
}

