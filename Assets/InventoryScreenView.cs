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

        foreach (var item in items)
        {
            var spawnedItem = Instantiate(inventoryItemPrefab, itemSpawnPoint);
            spawnedItem.Initialize(
                item,
                () =>
                {
                    DisplayPreview(item, inventoryManager);
                });
        }

        DisplayPreview(items.Count > 0 ? items[0] : null, inventoryManager);
    }

    private void DisplayPreview(ItemRuntime item, InventoryManager inventoryManager)
    {
        if(item == null)
        {
            inventoryManager.SetDisplayedItem(null);
            itemImage.sprite = null;
            itemDescription.text = "";
            return;
        }

        inventoryManager.SetDisplayedItem(item);
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
    }
}
