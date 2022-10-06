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

    public ItemRuntime displayedItem { get; private set; }

    public void Initialize(List<ItemRuntime> items)
    {
        foreach (var item in items)
        {
            var spawnedItem = Instantiate(inventoryItemPrefab, itemSpawnPoint);
            var itemCache = item;
            spawnedItem.Initialize(
                item,
                () =>
                {
                    DisplayPreview(itemCache);
                });
        }

        DisplayPreview(items[0]);
    }

    private void DisplayPreview(ItemRuntime item)
    {
        displayedItem = item;
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
    }
}
