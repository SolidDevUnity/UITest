using System;
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

        previewButton.interactable = hasSpawnedItems;
    }

    protected virtual void UpdateItemList(List<ItemRuntime> items, InventoryManager inventoryManager)
    {
        // spawn missing items
        var itemsToSpawn = items.Where(i => !spawnedItemsID.Any(siid => siid == i.itemDataStruct.itemID)).ToList();
        foreach (var item in itemsToSpawn)
        {
            var spawnedItem = Instantiate(itemPrefab, itemSpawnPoint) as InventoryItemController;
            spawnedItem.Initialize(
                item,
                () =>
                {
                    DisplayPreview(item, inventoryManager);
                });
            spawnedItemsID.Add(item.itemDataStruct.itemID);
        }

        // remove excess items
        var itemsToDestroyID = spawnedItemsID.Where(siid => !items.Any(i => i.itemDataStruct.itemID == siid)).ToList();
        foreach (Transform item in itemSpawnPoint)
        {
            int alreadySpawnedItemID = item.GetComponent<ItemController>().itemRuntime.itemDataStruct.itemID;
            bool itemIsToBeDestroyed = itemsToDestroyID.Any(its => its == alreadySpawnedItemID);
            if (itemIsToBeDestroyed)
            {
                Destroy(item.gameObject);
                spawnedItemsID.Remove(alreadySpawnedItemID);
            }
        }
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
