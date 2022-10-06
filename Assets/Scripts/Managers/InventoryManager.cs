using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : ScreenManager
{
    [Header("Item List")]
    [SerializeField]
    private RectTransform itemSpawnPoint;
    [SerializeField]
    private InventoryItemController inventoryItemPrefab;
    [SerializeField]
    private ItemPreviewScreenView itemPreviewScreen;

    [Space]
    [Header("Preview")]
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemDescription;
    private ItemRuntime displayedItem;

    private void Start()
    {
        var items = dataManager.GetInventoryItems();
        foreach (var item in items)
        {
            var spawnedItem = Instantiate(inventoryItemPrefab, itemSpawnPoint);
            spawnedItem.Initialize(item, this);
        }

        DisplayInPreviewArea(items[0]);
    }

    public override void OpenScreen()
    {
        uiManager.ToggleInventoryScreen(true);
    }

    public override void CloseScreen()
    {
        uiManager.ToggleInventoryScreen(false);
    }

    public void DisplayInPreviewArea(ItemRuntime item)
    {
        displayedItem = item;
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
    }

    public void OpenItemPreviewScreen()
    {
        uiManager.ToggleItemPreviewScreen(true);
        itemPreviewScreen.Initialize(displayedItem, this);
    }

    public void CloseItemPreviewScreen()
    {
        uiManager.ToggleItemPreviewScreen(false);
    }

    #region Put Up For Sale Item
    public void ShowPutUpForSaleItemScreen()
    {
        uiManager.TogglePutUpForSaleScreen(true);
    }

    public void PutUpItemForSale()
    {
        Debug.Log("PutUpItemForSale");
    }
    #endregion

    #region Delete Item
    public void ShowDeleteItemScreen()
    {
        uiManager.ToggleDeleteConfirmationWindow(true);
    }

    public void DeleteItem()
    {
        Debug.Log("delete item");
    }
    #endregion
}
