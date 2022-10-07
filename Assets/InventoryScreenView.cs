using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreenView : ScreenView
{
    [Header("Item List Area")]
    [SerializeField]
    private RectTransform itemSpawnPoint;
    [SerializeField]
    private InventoryItemController inventoryItemPrefab;

    [Space]
    [Header("Preview Area")]
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemDescription;

    // refactor later to check for differences instead of always destroying and spawning
    public void Initialize(List<ItemRuntime> items, InventoryManager inventoryManager)
    {
        foreach (Transform item in itemSpawnPoint)
        {
            Destroy(item.gameObject);
        }

        var cachedInventoryManager = inventoryManager;
        foreach (var item in items)
        {
            var spawnedItem = Instantiate(inventoryItemPrefab, itemSpawnPoint);
            var itemCache = item;
            spawnedItem.Initialize(
                item,
                () =>
                {
                    DisplayPreview(itemCache, cachedInventoryManager);
                });
        }

        DisplayPreview(items[0], cachedInventoryManager);
    }

    private void DisplayPreview(ItemRuntime item, InventoryManager inventoryManager)
    {
        inventoryManager.SetDisplayedItem(item);
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
    }
}
