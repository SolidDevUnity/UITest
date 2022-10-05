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

    [Space]
    [Header("Preview")]
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemDescription;

    private void Start()
    {
        var items = dataManager.GetInventoryItems();
        foreach (var item in items)
        {
            var spawnedItem = Instantiate(inventoryItemPrefab, itemSpawnPoint);
            spawnedItem.Initialize(item, this);
        }
    }

    public override void OpenScreen()
    {
        uiManager.ToggleInventoryScreen(true);
    }

    public override void CloseScreen()
    {
        uiManager.ToggleInventoryScreen(false);
    }

    public void ShowPreview(ItemRuntime item)
    {
        itemImage.sprite = item.itemImage;
        itemDescription.text = item.itemDescription;
    }
}
