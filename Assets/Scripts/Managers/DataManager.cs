using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private PlayerPrefsStorageService storageServiceComponent;
    [SerializeField]
    private StarterSaveDataProvider initialSaveDataProviderComponent;
    [SerializeField]
    private ItemCatalog itemCatalogComponent;

    private IDataStorageService storageService;
    private IInitialSaveDataProvider initialSaveDataProvider;
    private IItemCatalog itemCatalog;

    private SaveData currentSaveData;

    public Action<int> OnGoldAmountUpdate;

    private void Awake()
    {
        storageService = GetService<IDataStorageService>(storageServiceComponent, nameof(storageServiceComponent));
        initialSaveDataProvider = GetService<IInitialSaveDataProvider>(initialSaveDataProviderComponent, nameof(initialSaveDataProviderComponent));
        itemCatalog = GetService<IItemCatalog>(itemCatalogComponent, nameof(itemCatalogComponent));

        LoadOrCreateSaveData();
    }

    #region Gold
    public int GetPlayerGold()
    {
        EnsureSaveDataLoaded();
        return currentSaveData.playerGold;
    }

    public void AddGold(int goldToAdd)
    {
        EnsureSaveDataLoaded();
        if (goldToAdd <= 0)
        {
            Debug.LogWarning($"Attempted to add a non-positive gold amount ({goldToAdd}). Operation ignored.");
            return;
        }

        var newTotal = currentSaveData.playerGold + (long)goldToAdd;
        currentSaveData.playerGold = newTotal > int.MaxValue ? int.MaxValue : (int)newTotal;
        PersistAndNotify();
    }

    public void DeductGold(int goldToDeduct)
    {
        EnsureSaveDataLoaded();
        if (goldToDeduct <= 0)
        {
            Debug.LogWarning($"Attempted to deduct a non-positive gold amount ({goldToDeduct}). Operation ignored.");
            return;
        }

        if (currentSaveData.playerGold < goldToDeduct)
        {
            Debug.LogWarning($"Attempted to deduct {goldToDeduct} gold, but only {currentSaveData.playerGold} available. Clamping to 0.");
            currentSaveData.playerGold = 0;
            PersistAndNotify();
            return;
        }

        currentSaveData.playerGold -= goldToDeduct;
        PersistAndNotify();
    }
    #endregion

    #region Market
    public List<ItemRuntime> GetMarketItems()
    {
        EnsureSaveDataLoaded();
        return BuildRuntimeItems(currentSaveData.marketItems);
    }

    public void AddMarketItem(ItemDataStruct itemDataStruct)
    {
        EnsureSaveDataLoaded();
        if (!ContainsItem(currentSaveData.marketItems, itemDataStruct.itemID))
        {
            currentSaveData.marketItems.Add(itemDataStruct);
            Persist();
        }
        else
        {
            Debug.LogWarning($"Market already contains item with id {itemDataStruct.itemID}.");
        }
    }

    public void RemoveMarketItem(ItemDataStruct itemDataStruct)
    {
        EnsureSaveDataLoaded();
        RemoveItem(currentSaveData.marketItems, itemDataStruct.itemID, "market");
        Persist();
    }
    #endregion

    #region Inventory
    public List<ItemRuntime> GetInventoryItems()
    {
        EnsureSaveDataLoaded();
        return BuildRuntimeItems(currentSaveData.inventoryItems);
    }

    public void AddInventoryItem(ItemDataStruct itemDataStruct)
    {
        EnsureSaveDataLoaded();
        if (!ContainsItem(currentSaveData.inventoryItems, itemDataStruct.itemID))
        {
            currentSaveData.inventoryItems.Add(itemDataStruct);
            Persist();
        }
        else
        {
            Debug.LogWarning($"Inventory already contains item with id {itemDataStruct.itemID}.");
        }
    }

    public void RemoveInventoryItem(ItemDataStruct itemDataStruct)
    {
        EnsureSaveDataLoaded();
        RemoveItem(currentSaveData.inventoryItems, itemDataStruct.itemID, "inventory");
        Persist();
    }
    #endregion

    #region Internal helpers
    private void LoadOrCreateSaveData()
    {
        if (storageService == null || initialSaveDataProvider == null || itemCatalog == null)
        {
            Debug.LogError("DataManager dependencies are not correctly configured.");
            currentSaveData = new SaveData
            {
                inventoryItems = new List<ItemDataStruct>(),
                marketItems = new List<ItemDataStruct>(),
                playerGold = 0
            };
            return;
        }

        currentSaveData = storageService.HasSaveData()
            ? storageService.Load()
            : CreateInitialSaveData();

        EnsureCollections(currentSaveData);
        NotifyGoldChanged();
    }

    private SaveData CreateInitialSaveData()
    {
        var saveData = initialSaveDataProvider.CreateInitialSaveData();
        EnsureCollections(saveData);
        storageService.Save(saveData);
        return saveData;
    }

    private void EnsureSaveDataLoaded()
    {
        if (currentSaveData == null)
        {
            LoadOrCreateSaveData();
        }
    }

    private void Persist()
    {
        storageService?.Save(currentSaveData);
    }

    private void PersistAndNotify()
    {
        Persist();
        NotifyGoldChanged();
    }

    private void NotifyGoldChanged()
    {
        OnGoldAmountUpdate?.Invoke(currentSaveData.playerGold);
    }

    private List<ItemRuntime> BuildRuntimeItems(IEnumerable<ItemDataStruct> items)
    {
        var runtimeItems = new List<ItemRuntime>();
        foreach (var item in items)
        {
            var template = itemCatalog.GetItemByName(item.itemName);
            if (template == null)
            {
                continue;
            }

            runtimeItems.Add(new ItemRuntime(template, item));
        }

        return runtimeItems;
    }

    private static bool ContainsItem(IEnumerable<ItemDataStruct> source, int itemId)
    {
        return source.Any(item => item.itemID == itemId);
    }

    private static void RemoveItem(List<ItemDataStruct> source, int itemId, string collectionName)
    {
        var index = source.FindIndex(item => item.itemID == itemId);
        if (index >= 0)
        {
            source.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning($"Attempted to remove item {itemId} from {collectionName}, but it was not found.");
        }
    }

    private static void EnsureCollections(SaveData saveData)
    {
        if (saveData.inventoryItems == null)
        {
            saveData.inventoryItems = new List<ItemDataStruct>();
        }

        if (saveData.marketItems == null)
        {
            saveData.marketItems = new List<ItemDataStruct>();
        }
    }

    private static TService GetService<TService>(Component component, string fieldName) where TService : class
    {
        if (component == null)
        {
            Debug.LogError($"Missing dependency for {typeof(TService).Name} (field {fieldName}).");
            return null;
        }

        if (component is TService service)
        {
            return service;
        }

        var resolved = component.GetComponent<TService>();
        if (resolved != null)
        {
            return resolved;
        }

        Debug.LogError($"{component.name} does not provide {typeof(TService).Name} (field {fieldName}).");
        return null;
    }
    #endregion
}
