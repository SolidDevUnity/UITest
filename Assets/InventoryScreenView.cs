using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreenView : ScreenView
{
    [Header("Item List")]
    [SerializeField]
    private RectTransform itemSpawnPoint;
    [SerializeField]
    private InventoryItemController inventoryItemPrefab;

    [Space]
    [Header("Preview")]
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemDescription;

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
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
    }
}
