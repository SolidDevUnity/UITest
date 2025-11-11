using System.Collections.Generic;
using UnityEngine;

public class ItemCatalog : MonoBehaviour, IItemCatalog
{
    [SerializeField]
    private ItemSOHolder itemDatabase;

    private readonly Dictionary<string, ItemSO> itemLookup = new Dictionary<string, ItemSO>();

    private void Awake()
    {
        BuildLookup();
    }

    public ItemSO GetItemByName(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            Debug.LogWarning("Requested item with null or empty name.");
            return null;
        }

        if (itemLookup.TryGetValue(itemName, out var item))
        {
            return item;
        }

        Debug.LogWarning($"Item '{itemName}' not found in catalog.");
        return null;
    }

    private void BuildLookup()
    {
        itemLookup.Clear();

        if (itemDatabase == null || itemDatabase.items == null)
        {
            Debug.LogWarning("Item database is not configured.");
            return;
        }

        foreach (var item in itemDatabase.items)
        {
            if (item == null)
            {
                continue;
            }

            if (itemLookup.ContainsKey(item.name))
            {
                Debug.LogWarning($"Duplicate item name detected: {item.name}");
                continue;
            }

            itemLookup.Add(item.name, item);
        }
    }
}

