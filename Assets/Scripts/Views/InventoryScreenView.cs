using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreenView : ScreenListView
{
    [Space]
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemDescription;
    [SerializeField]
    private Button previewButton;

    public void Initialize(List<ItemRuntime> items, InventoryManager inventoryManager)
    {
        RefreshItemList(
            items,
            (item, itemSpawnPoint, itemPrefab) =>
            {
                var spawnedItem = Instantiate(itemPrefab, itemSpawnPoint) as InventoryItemController;
                spawnedItem.Initialize(
                    item,
                    () =>
                    {
                        DisplayPreview(item, inventoryManager);
                    });
            });

        bool hasSpawnedItems = spawnedItemsID.Count > 0;
        if (hasSpawnedItems)
        {
            var itemToDisplay = items.First(i => spawnedItemsID.Any(siid => siid == i.itemDataStruct.itemID));
            DisplayPreview(itemToDisplay, inventoryManager);
        }
        else
        {
            DisplayPreview(null, inventoryManager);
        }

        previewButton.enabled = hasSpawnedItems;
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
