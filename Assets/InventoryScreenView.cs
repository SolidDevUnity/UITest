using System.Collections.Generic;
using System.Linq;
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

    private List<int> spawnedItemsID = new List<int>();

    public void Initialize(List<ItemRuntime> items, InventoryManager inventoryManager)
    {
        RefreshItemList(items, inventoryManager);

        if (spawnedItemsID.Count > 0)
        {
            var itemToDisplay = items.First(i => spawnedItemsID.Any(siid => siid == i.itemDataStruct.itemID));
            DisplayPreview(itemToDisplay, inventoryManager);
        }
        else
        {
            DisplayPreview(null, inventoryManager);
        }
    }

    private void RefreshItemList(List<ItemRuntime> items, InventoryManager inventoryManager)
    {
        // spawn missing items
        var itemsToSpawn = items.Where(i => !spawnedItemsID.Any(siid => siid == i.itemDataStruct.itemID)).ToList();
        foreach (var item in itemsToSpawn)
        {
            var spawnedItem = Instantiate(inventoryItemPrefab, itemSpawnPoint);
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
            int alreadySpawnedItemID = item.GetComponent<InventoryItemController>().itemRuntime.itemDataStruct.itemID;
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
